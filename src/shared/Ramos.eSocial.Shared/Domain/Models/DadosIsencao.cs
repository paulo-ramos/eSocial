namespace Ramos.eSocial.Shared.Domain.Models;

public class DadosIsencao
{
    public string IdeMinLei { get; set; }
    public string NrCertif { get; set; }
    public DateTime DtEmisCertif { get; set; }
    public DateTime DtVencCertif { get; set; }
    public string? NrProtRenov { get; set; }
    public DateTime? DtProtRenov { get; set; }
    public DateTime? DtDou { get; set; }
    public int? PagDou { get; set; }
    
    public DadosIsencao(
        string ideMinLei,
        string nrCertif,
        DateTime dtEmisCertif,
        DateTime dtVencCertif,
        string? nrProtRenov,
        DateTime? dtProtRenov,
        DateTime? dtDou,
        int? pagDou)
    {
        IdeMinLei = ideMinLei;
        NrCertif = nrCertif;
        DtEmisCertif = dtEmisCertif;
        DtVencCertif = dtVencCertif;
        NrProtRenov = nrProtRenov;
        DtProtRenov = dtProtRenov;
        DtDou = dtDou;
        PagDou = pagDou;
    }

}