using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Repositories;

namespace Ramos.eSocial.Tests.Versao_S_1._3.S_1000.Repository;

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