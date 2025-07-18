using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Models;

public class IdeEventoS1000
{
    public IdeEventoS1000(EAmbiente tpAmb, EProcEmissao procEmi, string verProc)
    {
        TpAmb = tpAmb;
        ProcEmi = procEmi;
        VerProc = verProc;
    }
    public EAmbiente TpAmb { get;  }
    public EProcEmissao ProcEmi { get; }
    public string VerProc { get; }
}