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
        var ideEvento = MapIdeEvento(ideEventoDto);
        var ideEmpregador = MapIdeEmpregador(ideEmpregadorDto);
        var inclusao = MapInclusao(inclusaoDto);

        return EvtInfoEmpregador.CriarInclusao(id, ideEvento, ideEmpregador, inclusao);
    }
    
    
    private static IdeEventoS1000 MapIdeEvento(IdeEventoS1000Dto dto) =>
        new(dto.TpAmb, dto.ProcEmi, dto.VerProc);

    private static IdeEmpregador MapIdeEmpregador(IdeEmpregadorDto dto) =>
        new(dto.TpInsc, dto.NrInsc);

    private static Inclusao MapInclusao(InclusaoDto dto) =>
        new()
        {
            IdePeriodo = new IdePeriodo(dto.IdePeriodo.IniValid.Date, dto.IdePeriodo.FimValid.Date),
            InfoCadastro = MapInfoCadastro(dto.InfoCadastro)
        };

    private static InfoCadastro MapInfoCadastro(InfoCadastroDto dto) =>
        new(
            dto.RazaoSocial,
            dto.NomeFantasia,
            dto.ClassTrib,
            dto.IndCoop,
            dto.IndConstr,
            dto.IndDesFolha,
            dto.IndOpcCP,
            dto.IndPorte,
            dto.IndOptRegEletron,
            dto.CnpjEFR,
            dto.DtTrans11096,
            dto.IndTribFolhaPisPasep,
            dto.IndPertIRRF,
            MapDadosIsencao(dto.DadosIsencao),
            MapInfoOrgInternacional(dto.InfoOrgInternacional)
        );

    private static DadosIsencao? MapDadosIsencao(DadosIsencaoDto? dto) =>
        dto is null ? null :
            new(
                dto.IdeMinLei,
                dto.NrCertif,
                dto.DtEmisCertif,
                dto.DtVencCertif,
                dto.NrProtRenov,
                dto.DtProtRenov,
                dto.DtDou,
                dto.PagDou
            );

    private static InfoOrgInternacional? MapInfoOrgInternacional(InfoOrgInternacionalDto? dto) =>
        dto is null ? null : new(dto.IndAcordoIsenMulta);
}