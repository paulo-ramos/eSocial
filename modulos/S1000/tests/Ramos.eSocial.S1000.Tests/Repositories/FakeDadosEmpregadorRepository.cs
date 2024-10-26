using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.Interfaces;

namespace Ramos.eSocial.S1000.Tests.Repositories;

public class FakeDadosEmpregadorRepository : IDadosEmpregadorRepository
{
    public List<DadosEmpregador> DadosEmpregador { get; private set; } = new List<DadosEmpregador>();
    
    public bool DocumentExists(string document)
    {
        return DadosEmpregador.Count > 0;
    }

    public void SaveDadosEmpregador(DadosEmpregador dadosEmpregador)
    {
        DadosEmpregador.Add(dadosEmpregador);
    }
}