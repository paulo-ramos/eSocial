using FluentValidation;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Util.Interface;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Mappers;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.Shared.Domain.Validation;
using Ramos.eSocial.Shared.Util.Interface;

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

    public async Task<EventoS1000Dto> HandleAsync(IncluirEventoS1000Command comando)
    {
        var dto = comando.EventoDto;
        
        //0. Gera o Id do evento de inclusão 
        var id = _idGenerator.Gerar(dto.IdeEmpregador.NrInsc, CodigoEvento);

        
        // 1. Valida se o DTO contém os dados obrigatórios
        if (dto.IdeEvento is null || dto.IdeEmpregador is null || dto.Inclusao is null)
            throw new ArgumentException("Dados obrigatórios ausentes no comando.");

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

        // 4. Persiste
        await _repositorio.IncluirAsync(evento);

        // 5. Retorna DTO com ID preenchido
        return dto with { Id = evento.Id };
    }
}