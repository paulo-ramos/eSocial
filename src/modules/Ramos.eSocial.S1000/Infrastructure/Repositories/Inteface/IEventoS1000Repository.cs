using Ramos.eSocial.Shared.Domain.Models;
using Ramos.eSocial.Shared.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;

public interface IEventoS1000Repository
{
    Task IncluirAsync(EvtInfoEmpregador eventoDominio);
    
    Task<List<EvtInfoEmpregador>> ObterHistoricoPorCnpj(string cnpj);
    Task<EvtInfoEmpregador?> ObterVigentePorCnpj(string cnpj, DateTime? referencia = null);
    Task<EvtInfoEmpregador?> ObterPorNrInscAsync(string nrInsc);

    Task<EventoAnterior?> ObterVigenciaAnteriorAsync(string cnpj, DateTime novaIniValid);
    Task FecharVigenciaAsync(string id, DateTime novaFimValid);
    Task<EvtInfoEmpregador?> ObterPorIdAsync(string id);

}