using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.Interfaces;

namespace Ramos.eSocial.S1000.Infrastructure.Repositories;

public class DadosEmpregadorRepository: IDadosEmpregadorRepository
{
    private readonly List<DadosEmpregador> _dadosEmpregados;
    public bool DocumentExists(string document)
    {
        return _dadosEmpregados.Count > 0 && _dadosEmpregados.Any(e => e.IdeEmpregador.NrInsc == document);
    }

    public void SaveDadosEmpregador(DadosEmpregador dadosEmpregador)
    {
        _dadosEmpregados.Add(dadosEmpregador);
    }
}