using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Entities;

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