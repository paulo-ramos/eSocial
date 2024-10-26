using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Repositories;

public interface IDadosEmpregadorRepository
{
    bool DocumentExists(string document);
    void SaveDadosEmpregador(DadosEmpregador dadosEmpregador);
}