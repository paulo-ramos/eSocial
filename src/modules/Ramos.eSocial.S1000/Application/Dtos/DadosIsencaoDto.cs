namespace Ramos.eSocial.S1000.Application.Dtos;

public record class DadosIsencaoDto
{
    public string IdeMinLei { get; init; } = string.Empty;
    public string NrCertif { get; init; } = string.Empty;
    public DateTime DtEmisCertif { get; init; }
    public DateTime DtVencCertif { get; init; }
    public string? NrProtRenov { get; init; }
    public DateTime? DtProtRenov { get; init; }
    public DateTime? DtDou { get; init; }
    public int? PagDou { get; init; }
}