using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.S1000.Application.Mappers;

public class EventoS1000AlteracaoMapper
{
        public static EvtInfoEmpregador MapToDomain(
        string id,
        IdeEventoS1000Dto ideEventoDto,
        IdeEmpregadorDto ideEmpregadorDto,
        AlteracaoDto alteracaoDto)
    {
        var ideEvento = new IdeEventoS1000(
            ideEventoDto.TpAmb,
            ideEventoDto.ProcEmi,
            ideEventoDto.VerProc);

        var ideEmpregador = new IdeEmpregador(
            ideEmpregadorDto.TpInsc,
            ideEmpregadorDto.NrInsc);

        var idePeriodo = new IdePeriodo(
            alteracaoDto.IdePeriodo.IniValid.Date,
            alteracaoDto.IdePeriodo.FimValid.Date);

        var dadosIsencao = alteracaoDto.InfoCadastro.DadosIsencao is not null
            ? new DadosIsencao(
                alteracaoDto.InfoCadastro.DadosIsencao.IdeMinLei,
                alteracaoDto.InfoCadastro.DadosIsencao.NrCertif,
                alteracaoDto.InfoCadastro.DadosIsencao.DtEmisCertif,
                alteracaoDto.InfoCadastro.DadosIsencao.DtVencCertif,
                alteracaoDto.InfoCadastro.DadosIsencao.NrProtRenov,
                alteracaoDto.InfoCadastro.DadosIsencao.DtProtRenov,
                alteracaoDto.InfoCadastro.DadosIsencao.DtDou,
                alteracaoDto.InfoCadastro.DadosIsencao.PagDou)
            : null;

        var infoOrgInternacional = alteracaoDto.InfoCadastro.InfoOrgInternacional is not null
            ? new InfoOrgInternacional(
                alteracaoDto.InfoCadastro.InfoOrgInternacional.IndAcordoIsenMulta)
            : null;

        var infoCadastro = new InfoCadastro(
            alteracaoDto.InfoCadastro.RazaoSocial,
            alteracaoDto.InfoCadastro.NomeFantasia,
            alteracaoDto.InfoCadastro.ClassTrib,
            alteracaoDto.InfoCadastro.IndCoop,
            alteracaoDto.InfoCadastro.IndConstr,
            alteracaoDto.InfoCadastro.IndDesFolha,
            alteracaoDto.InfoCadastro.IndOpcCP,
            alteracaoDto.InfoCadastro.IndPorte,
            alteracaoDto.InfoCadastro.IndOptRegEletron,
            alteracaoDto.InfoCadastro.CnpjEFR,
            alteracaoDto.InfoCadastro.DtTrans11096,
            alteracaoDto.InfoCadastro.IndTribFolhaPisPasep,
            alteracaoDto.InfoCadastro.IndPertIRRF,
            dadosIsencao,
            infoOrgInternacional);

        var alteracao = new Alteracao
        {
            IdePeriodo = idePeriodo,
            InfoCadastro = infoCadastro
        };

        return EvtInfoEmpregador.CriarAlteracao(
            id,
            ideEvento,
            ideEmpregador,
            alteracao);
    }
}