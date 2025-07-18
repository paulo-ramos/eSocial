using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Models;

public class IdeEmpregador
{
    public IdeEmpregador(ETipoInscricao tpInsc, string nrInsc)
    {
        TpInsc = tpInsc;
        NrInsc = nrInsc;
    }

    public ETipoInscricao TpInsc { get; }
    public string NrInsc { get; }
}