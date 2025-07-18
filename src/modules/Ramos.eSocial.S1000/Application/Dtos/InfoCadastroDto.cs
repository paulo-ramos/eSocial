using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.S1000.Application.Dtos;

public record class InfoCadastroDto
{
    public string RazaoSocial { get; init; } = string.Empty;
    public string NomeFantasia { get; init; } = string.Empty;
    public EClassTributaria ClassTrib { get; init; }
    public EIndicativoCooperativa? IndCoop { get; init; }
    public EIndicativoConstrutora? IndConstr { get; init; }
    public EDesoneracaoFolha IndDesFolha { get; init; }
    public EOpcaoCP? IndOpcCP { get; init; }
    public string? IndPorte { get; init; }
    public ERegistroEletronico IndOptRegEletron { get; init; }
    public string? CnpjEFR { get; init; }
    public DateTime? DtTrans11096 { get; init; }
    public string? IndTribFolhaPisPasep { get; init; }
    public string? IndPertIRRF { get; init; }
    public DadosIsencaoDto? DadosIsencao { get; init; }
    public InfoOrgInternacionalDto? InfoOrgInternacional { get; init; }
}
