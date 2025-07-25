using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Domain.Models;
using Ramos.eSocial.Shared.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Infrastructure.Repositories;

public class EventoS1000Repository : IEventoS1000Repository
{
    private readonly List<EvtInfoEmpregador> _eventos = new();

    public Task IncluirAsync(EvtInfoEmpregador evento)
    {
        _eventos.Add(evento);
        return Task.CompletedTask;
    }

    public Task<List<EvtInfoEmpregador>> ObterHistoricoPorCnpj(string cnpj)
    {
        var resultado = _eventos
            .Where(e => e.IdeEmpregador.NrInsc == cnpj)
            .OrderBy(e => e.Id)
            .ToList();

        return Task.FromResult(resultado);
    }

    public async Task<EvtInfoEmpregador?> ObterVigentePorCnpj(string cnpj, DateTime? referencia = null)
    {
        var data = referencia ?? DateTime.Today;
        var historico = await ObterHistoricoPorCnpj(cnpj);

        var vigente = historico
            .Where(e => e.InfoEmpregador.Alteracao.IdePeriodo.IniValid <= data)
            .OrderByDescending(e => e.InfoEmpregador.Alteracao.IdePeriodo.IniValid)
            .FirstOrDefault();

        return vigente;
    }

    public async Task<EvtInfoEmpregador?> ObterPorNrInscAsync(string nrInsc)
    {
        var historico = await ObterHistoricoPorCnpj(nrInsc);

        var inclusao = historico
            .Where(e => e.InfoEmpregador.Inclusao is not null &&
                        e.IdeEmpregador.NrInsc == nrInsc)
            .OrderByDescending(e => e.InfoEmpregador.Inclusao.IdePeriodo.IniValid)
            .FirstOrDefault();

        return inclusao;
    }
    
    public async Task<EventoAnterior?> ObterVigenciaAnteriorAsync(string cnpj, DateTime novaIniValid)
    {
        var historico = await ObterHistoricoPorCnpj(cnpj);

        var anterior = historico
            .Select(e => new {
                Evento = e,
                Vigencia = e.InfoEmpregador.GetVigencia()
            })
            .Where(x => x.Vigencia is not null && x.Vigencia.IniValid < novaIniValid)
            .OrderByDescending(x => x.Vigencia!.IniValid)
            .FirstOrDefault();

        return anterior is null || anterior.Vigencia is null
            ? null
            : new EventoAnterior(anterior.Evento.Id, anterior.Vigencia);
    }

    public async Task<EvtInfoEmpregador?> ObterPorIdAsync(string id)
    {
        return _eventos.FirstOrDefault(e => e.Id == id);
    }

    public async Task FecharVigenciaAsync(string id, DateTime novaFimValid)
    {
        var evento = await ObterPorIdAsync(id);
        if (evento is null) return;

        evento.FecharVigencia(novaFimValid);
    }
}