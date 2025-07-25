using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.S1000.Application.Mappers;

public class EventoS1000ExclusaoMapper
{
        public static EvtInfoEmpregador MapToDomain(
        string id,
        IdeEventoS1000Dto ideEventoDto,
        IdeEmpregadorDto ideEmpregadorDto,
        ExclusaoDto exclusaoDto)
    {
        var ideEvento = new IdeEventoS1000(
            ideEventoDto.TpAmb,
            ideEventoDto.ProcEmi,
            ideEventoDto.VerProc);

        var ideEmpregador = new IdeEmpregador(
            ideEmpregadorDto.TpInsc,
            ideEmpregadorDto.NrInsc);

        var idePeriodo = new IdePeriodo(
            exclusaoDto.IdePeriodo.IniValid,
            exclusaoDto.IdePeriodo.FimValid);

        var exclusao = new Exclusao
        {
            IdePeriodo = idePeriodo
        };

        return EvtInfoEmpregador.CriarExclusao(
            id,
            ideEvento,
            ideEmpregador,
            exclusao);
    }
}