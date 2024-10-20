using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Enums;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

public class InfoOrgInternacional
{
    public InfoOrgInternacional(EIndAcordoIsenMulta indAcordoIsenMulta)
    {
        IndAcordoIsenMulta = indAcordoIsenMulta;
    }

    public EIndAcordoIsenMulta IndAcordoIsenMulta { get; private set; }
}