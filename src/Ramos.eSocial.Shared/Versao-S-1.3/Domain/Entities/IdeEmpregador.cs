using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Enums;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

public class IdeEmpregador
{
    public IdeEmpregador(ETpInsc tpInsc, string nrInsc)
    {
        TpInsc = tpInsc;
        NrInsc = nrInsc;
    }

    public ETpInsc TpInsc { get; private set; }
    public string NrInsc { get; private set; }
}