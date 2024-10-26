namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

public class InfoCadastro
{
    public InfoCadastro(string classTrib, int indCoop, int indConstr, int indDesFolha, int indOpcCp, string indPorte, int indOptRegEletron, string cnpjEfr, DateTime dtTrans11096, string indTribFolhaPisPasep)
    {
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
    }

    public string ClassTrib { get; private set; }
    public int IndCoop { get; private set; }
    public int IndConstr { get; private set; }
    public int IndDesFolha { get; private set; }
    public int IndOpcCp { get; private set; }
    public string  IndPorte { get; private set; }
    public int IndOptRegEletron { get; private set; }
    public string CnpjEfr { get; private set; }
    public DateTime DtTrans11096 { get; private set; }
    public string IndTribFolhaPisPasep { get; private set; }
}