using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;

public interface IEventoS1000Repository
{
    Task IncluirAsync(EvtInfoEmpregador eventoDominio);
}