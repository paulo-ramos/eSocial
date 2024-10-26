using Ramos.eSocial.S1000.Domain.Entities;

namespace Ramos.eSocial.S1000.Domain.Interfaces;

public interface IDadosEmpregadorRepository
{
    bool DocumentExists(string document);
    void SaveDadosEmpregador(DadosEmpregador dadosEmpregador);
}