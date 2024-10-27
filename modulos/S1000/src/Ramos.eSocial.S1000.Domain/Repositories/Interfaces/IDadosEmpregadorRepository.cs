using Ramos.eSocial.S1000.Domain.Entities;

namespace Ramos.eSocial.S1000.Domain.Interfaces;

public interface IDadosEmpregadorRepository
{
    Task<List<DadosEmpregador>> GetAllAsync();

    Task<DadosEmpregador?> GetByIdAsync(string id);
    Task<bool> ExistsAsync(string id);

    Task CreateAsync(DadosEmpregador dadosEmpregador);

    Task UpdateAsync(string id, DadosEmpregador dadosEmpregadorIn);

    Task DeleteAsync(string id); 
}