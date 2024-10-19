using Ramos.eSocial.Shared.Domain.Base;

namespace Ramos.eSocial.Shared.Domain.Entities;

public class DadosIsencao
{
    public DadosIsencao(string ideMinLei, string nrCertif, DateTime dtEmisCertif, DateTime dtVencCertif, string nrProtRenov, DateTime dtProtRenov, DateTime dtDou, int pagDou)
    {
        this.ideMinLei = ideMinLei;
        this.nrCertif = nrCertif;
        this.dtEmisCertif = dtEmisCertif;
        this.dtVencCertif = dtVencCertif;
        this.nrProtRenov = nrProtRenov;
        this.dtProtRenov = dtProtRenov;
        this.dtDou = dtDou;
        this.pagDou = pagDou;
    }

    public string  ideMinLei { get; private set; }
    public string nrCertif { get; private set; }
    public DateTime dtEmisCertif { get; private set; }
    public DateTime dtVencCertif { get; private set; }
    public string nrProtRenov { get; private set; }
    public DateTime dtProtRenov { get; private set; }
    public DateTime dtDou { get; private set; }
    public int pagDou { get; private set; }
}