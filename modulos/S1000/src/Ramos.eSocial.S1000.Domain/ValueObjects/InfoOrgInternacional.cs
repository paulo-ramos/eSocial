using Ramos.eSocial.S1000.Domain.Enums;

namespace Ramos.eSocial.S1000.Domain.ValueObjects;

public class InfoOrgInternacional
{
    public InfoOrgInternacional(EIndAcordoIsenMulta indAcordoIsenMulta)
    {
        IndAcordoIsenMulta = indAcordoIsenMulta;
    }

    public EIndAcordoIsenMulta IndAcordoIsenMulta { get; private set; }
}