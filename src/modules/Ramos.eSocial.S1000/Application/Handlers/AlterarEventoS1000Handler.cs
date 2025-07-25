using System.Transactions;
using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.S1000.Application.Mappers;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Domain.Validation;
using Ramos.eSocial.Shared.Util.Interface;

namespace Ramos.eSocial.S1000.Application.Handlers;

public class AlterarEventoS1000Handler
{
    private const string CodigoEvento = "S1000";
    private readonly IEventoS1000Repository _repositorio;
    private readonly IEventIdGenerator _idGenerator;

    public AlterarEventoS1000Handler(
        IEventoS1000Repository repositorio,
        IEventIdGenerator idGenerator)
    {
        _repositorio = repositorio;
        _idGenerator = idGenerator;
    }

    public async Task<EventoS1000AlteracaoDto> HandleAsync(AlterarEventoS1000Command comando)
    {
        var dto = comando.EventoDto;
        
        // 0. Valida se o DTO contém os dados obrigatórios
        if (dto.IdeEvento is null || dto.IdeEmpregador is null || dto.Alteracao is null || string.IsNullOrWhiteSpace(dto.IdeEmpregador.NrInsc))
            throw new ArgumentException("Dados obrigatórios ausentes no comando.");
        
        // 1. Verifica a existência do dado a ser alterado
        var historico = await _repositorio.ObterHistoricoPorCnpj(dto.IdeEmpregador.NrInsc);

        if (historico.Count == 0)
            throw new InvalidOperationException("Alteração não permitida: não há eventos anteriores para esse CNPJ.");
        
        // 2. Verifica se há sobreposição de períodos
        var iniNova = dto.Alteracao.IdePeriodo.IniValid.Date;

        if (historico.Any(e =>
                e.InfoEmpregador.Alteracao is not null &&
                (e.InfoEmpregador.Alteracao.IdePeriodo.IniValid == iniNova ||
                 (e.InfoEmpregador.Alteracao.IdePeriodo.FimValid.Date < DateTime.MaxValue.Date &&
                  e.InfoEmpregador.Alteracao.IdePeriodo.FimValid.Date >= iniNova))))
        {
            throw new InvalidOperationException($"Já existe evento de alteração para o período {iniNova:yyyy-MM}.");
        }
        
        // 3. Gera o Id do evento de Alteração 
        var id = _idGenerator.Gerar(dto.IdeEmpregador.NrInsc, CodigoEvento);

        
        // 4. Mapeia DTO → Domínio
        var evento = EventoS1000AlteracaoMapper.MapToDomain(
            id,
            dto.IdeEvento,
            dto.IdeEmpregador,
            dto.Alteracao);

        // 5. Valida o modelo de domínio
        var resultadoValidacao = new EvtInfoEmpregadorValidator().Validate(evento);
        if (!resultadoValidacao.IsValid)
        {
            var mensagens = string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Evento inválido: {mensagens}");
        }

        // 6. Persiste
        // Transação
        using var transacao = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var anterior = await _repositorio.ObterVigenciaAnteriorAsync(dto.IdeEmpregador.NrInsc, iniNova);

        if (anterior is not null)
        {
            var fimValid = iniNova.AddDays(-1).Date;
            await _repositorio.FecharVigenciaAsync(anterior.Id, fimValid);
        }

        await _repositorio.IncluirAsync(evento);

        transacao.Complete();



        // 7. Retorna DTO com ID preenchido
        if (evento.InfoEmpregador == null)
            throw new ArgumentNullException(nameof(evento.InfoEmpregador), "InfoEmpregador não pode ser nulo.");

        return dto with
        {
            Id = id,
            Alteracao = dto.Alteracao with
            {
                IdePeriodo = dto.Alteracao.IdePeriodo with
                {
                    FimValid = evento.InfoEmpregador.GetVigencia()!.FimValid
                }
            }
        };
    }
}