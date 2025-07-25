using System.Transactions;
using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.S1000.Application.Mappers;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Domain.Validation;
using Ramos.eSocial.Shared.Util.Interface;

public class ExcluirEventoS1000Handler
{
    private const string CodigoEvento = "S1000";
    private readonly IEventoS1000Repository _repositorio;
    private readonly IEventIdGenerator _idGenerator;

    public ExcluirEventoS1000Handler(
        IEventoS1000Repository repositorio,
        IEventIdGenerator idGenerator)
    {
        _repositorio = repositorio;
        _idGenerator = idGenerator;
    }

    public async Task<EventoS1000ExclusaoDto> HandleAsync(ExcluirEventoS1000Command comando)
    {
        var dto = comando.EventoDto;

        // 0. Valida presença dos dados obrigatórios
        if (dto.IdeEvento is null || dto.IdeEmpregador is null || dto.Exclusao is null || string.IsNullOrWhiteSpace(dto.IdeEmpregador.NrInsc))
            throw new ArgumentException("Dados obrigatórios ausentes no comando.");

        // 1. Consulta histórico
        var historico = await _repositorio.ObterHistoricoPorCnpj(dto.IdeEmpregador.NrInsc);

        var possuiInclusaoAnterior = historico.Any(e => e.InfoEmpregador.Inclusao is not null);

        if (!possuiInclusaoAnterior)
            throw new InvalidOperationException("Exclusão não permitida: o CNPJ informado não possui evento de inclusão prévio.");

        // 2. Verifica se já existe exclusão para o período informado
        var iniExclusao = dto.Exclusao.IdePeriodo.IniValid;

        var existeDuplicidade = historico.Any(e =>
            e.InfoEmpregador.Exclusao is not null &&
            e.InfoEmpregador.Exclusao.IdePeriodo.IniValid == iniExclusao);

        if (existeDuplicidade)
            throw new InvalidOperationException($"Já existe evento de exclusão para o período {iniExclusao:yyyy-MM}.");

        // 3. Gera novo ID
        var id = _idGenerator.Gerar(dto.IdeEmpregador.NrInsc, CodigoEvento);

        // 4. Mapeia DTO → Domínio
        var evento = EventoS1000ExclusaoMapper.MapToDomain(
            id,
            dto.IdeEvento,
            dto.IdeEmpregador,
            dto.Exclusao);

        // 5. Valida
        var resultadoValidacao = new EvtInfoEmpregadorValidator().Validate(evento);
        if (!resultadoValidacao.IsValid)
        {
            var mensagens = string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Evento inválido: {mensagens}");
        }

        // 6. Persiste
        using var transacao = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var anterior = await _repositorio.ObterVigenciaAnteriorAsync(dto.IdeEmpregador.NrInsc, iniExclusao);

        if (anterior is not null)
        {
            var fimValid = iniExclusao.AddDays(-1);
            await _repositorio.FecharVigenciaAsync(anterior.Id, fimValid);
        }

        await _repositorio.IncluirAsync(evento);

        // 7. Retorna DTO com ID preenchido
        if (evento.InfoEmpregador == null)
            throw new ArgumentNullException(nameof(evento.InfoEmpregador), "InfoEmpregador não pode ser nulo.");

        return dto with
        {
            Id = id,
            Exclusao = dto.Exclusao with
            {
                IdePeriodo = dto.Exclusao.IdePeriodo with
                {
                    FimValid = evento.InfoEmpregador.GetVigencia()!.FimValid
                }
            }
        };
    }
}