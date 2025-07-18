using System;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.S1000.Application.Mappers;

public static class EventoS1000InclusaoMapper
{
    public static EvtInfoEmpregador MapToDomain(
        string id,
        IdeEventoS1000Dto ideEventoDto,
        IdeEmpregadorDto ideEmpregadorDto,
        InclusaoDto inclusaoDto)
    {
        var ideEvento = new IdeEventoS1000(
            ideEventoDto.TpAmb,
            ideEventoDto.ProcEmi,
            ideEventoDto.VerProc);

        var ideEmpregador = new IdeEmpregador(
            ideEmpregadorDto.TpInsc,
            ideEmpregadorDto.NrInsc);

        var idePeriodo = new IdePeriodo(
            inclusaoDto.IdePeriodo.IniValid,
            inclusaoDto.IdePeriodo.FimValid);

        var dadosIsencao = inclusaoDto.InfoCadastro.DadosIsencao is not null
            ? new DadosIsencao(
                inclusaoDto.InfoCadastro.DadosIsencao.IdeMinLei,
                inclusaoDto.InfoCadastro.DadosIsencao.NrCertif,
                inclusaoDto.InfoCadastro.DadosIsencao.DtEmisCertif,
                inclusaoDto.InfoCadastro.DadosIsencao.DtVencCertif,
                inclusaoDto.InfoCadastro.DadosIsencao.NrProtRenov,
                inclusaoDto.InfoCadastro.DadosIsencao.DtProtRenov,
                inclusaoDto.InfoCadastro.DadosIsencao.DtDou,
                inclusaoDto.InfoCadastro.DadosIsencao.PagDou)
            : null;

        var infoOrgInternacional = inclusaoDto.InfoCadastro.InfoOrgInternacional is not null
            ? new InfoOrgInternacional(
                inclusaoDto.InfoCadastro.InfoOrgInternacional.IndAcordoIsenMulta)
            : null;

        var infoCadastro = new InfoCadastro(
            inclusaoDto.InfoCadastro.ClassTrib,
            inclusaoDto.InfoCadastro.IndCoop,
            inclusaoDto.InfoCadastro.IndConstr,
            inclusaoDto.InfoCadastro.IndDesFolha,
            inclusaoDto.InfoCadastro.IndOpcCP,
            inclusaoDto.InfoCadastro.IndPorte,
            inclusaoDto.InfoCadastro.IndOptRegEletron,
            inclusaoDto.InfoCadastro.CnpjEFR,
            inclusaoDto.InfoCadastro.DtTrans11096,
            inclusaoDto.InfoCadastro.IndTribFolhaPisPasep,
            inclusaoDto.InfoCadastro.IndPertIRRF,
            dadosIsencao,
            infoOrgInternacional);

        var inclusao = new Inclusao
        {
            IdePeriodo = idePeriodo,
            InfoCadastro = infoCadastro
        };

        return EvtInfoEmpregador.CriarInclusao(
            id,
            ideEvento,
            ideEmpregador,
            inclusao);
    }
}