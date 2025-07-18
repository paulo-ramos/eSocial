using Ramos.eSocial.Shared.Util.Interface;

namespace Ramos.eSocial.Shared.Domain.Models;

public class EvtInfoEmpregador
{
    public string Id { get; set; }
    public IdeEventoS1000 IdeEvento { get; set; }
    public IdeEmpregador IdeEmpregador { get; set; }
    public InfoEmpregador InfoEmpregador { get; set; }
    
    
    public static EvtInfoEmpregador CriarInclusao(
        string id,
        IdeEventoS1000 ideEvento,
        IdeEmpregador ideEmpregador,
        Inclusao inclusao)
    {
        return new EvtInfoEmpregador
        {
            Id = id,
            IdeEvento = ideEvento,
            IdeEmpregador = ideEmpregador,
            InfoEmpregador = new InfoEmpregador { Inclusao = inclusao }
        };
    }
    
    
    
    public static EvtInfoEmpregador CriarAlteracao(
        IdeEventoS1000 ideEvento,
        IdeEmpregador ideEmpregador,
        Alteracao alteracao,
        IEventIdGenerator idGen)
    {
        return new EvtInfoEmpregador
        {
            Id = idGen.Gerar(ideEmpregador.NrInsc, "S1000"),
            IdeEvento = ideEvento,
            IdeEmpregador = ideEmpregador,
            InfoEmpregador = new InfoEmpregador { Alteracao = alteracao }
        };
    }
    
    
    
    public static EvtInfoEmpregador CriarExclusao(
        IdeEventoS1000 ideEvento,
        IdeEmpregador ideEmpregador,
        Exclusao exclusao,
        IEventIdGenerator idGen)
    {
        return new EvtInfoEmpregador
        {
            Id = idGen.Gerar(ideEmpregador.NrInsc, "S1000"),
            IdeEvento = ideEvento,
            IdeEmpregador = ideEmpregador,
            InfoEmpregador = new InfoEmpregador { Exclusao = exclusao }
        };
    }
    
    
        
}