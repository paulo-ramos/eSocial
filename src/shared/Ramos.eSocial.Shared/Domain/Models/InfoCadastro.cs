using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Shared.Domain.Models;

public class InfoCadastro
{
    public InfoCadastro(
        string razaoSocial,
        string nomeFantasia,
        EClassTributaria classTrib,
        EIndicativoCooperativa? indCoop,
        EIndicativoConstrutora? indConstr,
        EDesoneracaoFolha indDesFolha,
        EOpcaoCP? indOpcCp,
        string? indPorte,
        ERegistroEletronico indOptRegEletron,
        string? cnpjEfr,
        DateTime? dtTrans11096,
        string? indTribFolhaPisPasep,
        string? indPertIrrf,
        DadosIsencao? dadosIsencao,
        InfoOrgInternacional? infoOrgInternacional)
    {
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        ClassTrib = classTrib;
        IndCoop = indCoop;
        IndConstr = indConstr;
        IndDesFolha = indDesFolha;
        IndOpcCp = indOpcCp;
        IndPorte = indPorte;
        IndOptRegEletron = indOptRegEletron;
        CnpjEfr = cnpjEfr;
        DtTrans11096 = dtTrans11096;
        IndTribFolhaPisPasep = indTribFolhaPisPasep;
        IndPertIrrf = indPertIrrf;
        DadosIsencao = dadosIsencao;
        InfoOrgInternacional = infoOrgInternacional;
    }

    public string RazaoSocial { get; }
    public string NomeFantasia { get; }
    public EClassTributaria ClassTrib { get; }
    public EIndicativoCooperativa? IndCoop { get; }
    public EIndicativoConstrutora? IndConstr { get; }
    public EDesoneracaoFolha IndDesFolha { get; }
    public EOpcaoCP? IndOpcCp { get; }
    public string? IndPorte { get; }
    public ERegistroEletronico IndOptRegEletron { get; }
    public string? CnpjEfr { get; }
    public DateTime? DtTrans11096 { get; }
    public string? IndTribFolhaPisPasep { get; }
    public string? IndPertIrrf { get; }
    public DadosIsencao? DadosIsencao { get; }
    public InfoOrgInternacional? InfoOrgInternacional { get; }
}