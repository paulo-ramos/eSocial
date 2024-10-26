using Ramos.eSocial.S1000.Domain.Enums;

namespace Ramos.eSocial.S1000.Domain.ValueObjects;

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