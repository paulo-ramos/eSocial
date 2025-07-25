using FluentValidation;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Util.Interface;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Mappers;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.Shared.Domain.Validation;

namespace Ramos.eSocial.S1000.Application.Handlers;

public class IncluirEventoS1000Handler
{
    private const string CodigoEvento = "S1000";
    private readonly IEventoS1000Repository _repositorio;
    private readonly IEventIdGenerator _idGenerator;

    public IncluirEventoS1000Handler(
        IEventoS1000Repository repositorio,
        IEventIdGenerator idGenerator)
    {
        _repositorio = repositorio;
        _idGenerator = idGenerator;
    }

    public async Task<EventoS1000InclusaoDto> HandleAsync(IncluirEventoS1000Command comando)
    {
        var dto = comando.EventoDto;
        
        // 0. Valida se o DTO contém os dados obrigatórios
        if (dto.IdeEvento is null || dto.IdeEmpregador is null || dto.Inclusao is null || string.IsNullOrWhiteSpace(dto.IdeEmpregador.NrInsc))
            throw new ArgumentException("Dados obrigatórios ausentes.");
        
        // 1. Gera o Id do evento de inclusão 
        var id = _idGenerator.Gerar(dto.IdeEmpregador.NrInsc, CodigoEvento);

        // 2. Mapeia DTO → Domínio
        var evento = EventoS1000InclusaoMapper.MapToDomain(
            id,
            dto.IdeEvento,
            dto.IdeEmpregador,
            dto.Inclusao);

        // 3. Valida o modelo de domínio
        var resultadoValidacao = new EvtInfoEmpregadorValidator().Validate(evento);
        if (!resultadoValidacao.IsValid)
        {
            var mensagens = string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Evento inválido: {mensagens}");
        }

        // 4 Verifica se o CNPJ já está cadastrado
        var existente = await _repositorio.ObterPorNrInscAsync(dto.IdeEmpregador.NrInsc);
        if (existente is not null)
        {
            throw new ValidationException($"CNPJ já cadastrado com o ID: {existente.Id}");
        }

        // 5. Persiste
        await _repositorio.IncluirAsync(evento);

        // 6. Retorna DTO com ID preenchido
        if (evento.InfoEmpregador == null)
            throw new ArgumentNullException(nameof(evento.InfoEmpregador), "InfoEmpregador não pode ser nulo.");

        return dto with
        {
            Id = id,
            Inclusao = dto.Inclusao with
            {
                IdePeriodo = dto.Inclusao.IdePeriodo with
                {
                    FimValid = evento.InfoEmpregador.GetVigencia()!.FimValid
                }
            }
        };
    }
}