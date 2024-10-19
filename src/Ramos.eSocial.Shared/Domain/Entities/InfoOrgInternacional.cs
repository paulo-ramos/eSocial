using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Entities;

public class InfoOrgInternacional
{
    public InfoOrgInternacional(EIndAcordoIsenMulta indAcordoIsenMulta)
    {
        IndAcordoIsenMulta = indAcordoIsenMulta;
    }

    public EIndAcordoIsenMulta IndAcordoIsenMulta { get; private set; }
}