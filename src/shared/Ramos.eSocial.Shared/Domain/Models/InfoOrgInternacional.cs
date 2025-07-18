using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Models;

public class InfoOrgInternacional
{
    public EAcordoIsencaoMulta IndAcordoIsenMulta { get; set; }

    public InfoOrgInternacional(EAcordoIsencaoMulta indAcordoIsenMulta)
    {
        IndAcordoIsenMulta = indAcordoIsenMulta;
    }
}