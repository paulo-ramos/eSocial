using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.Interfaces;

namespace Ramos.eSocial.S1000.Tests.Repositories;

public class FakeDadosEmpregadorRepository : IDadosEmpregadorRepository
{
    public List<DadosEmpregador> DadosEmpregador { get; private set; } = new List<DadosEmpregador>();
    
    public async Task<List<DadosEmpregador>> GetAllAsync()
    {
        return this.DadosEmpregador;
    }

    public async Task<DadosEmpregador?> GetByIdAsync(string id)
    {
        var result =  DadosEmpregador.FirstOrDefault(x => x.IdeEmpregador.NrInsc  == id);
        return result;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var result =  DadosEmpregador.FirstOrDefault(x => x.IdeEmpregador.NrInsc  == id);
        return result == null;
    }

    public async Task CreateAsync(DadosEmpregador dadosEmpregador)
    {
        DadosEmpregador.Add(dadosEmpregador);
    }

    public async Task UpdateAsync(string id, DadosEmpregador dadosEmpregadorIn)
    {
        var empregadorParaAtualizar =  DadosEmpregador.FirstOrDefault(x => x.IdeEmpregador.NrInsc  == id);
        if (empregadorParaAtualizar != null)
        {
            empregadorParaAtualizar = dadosEmpregadorIn;
        }
    }

    public async Task DeleteAsync(string id)
    {
        var empregadorParaAtualizar =  DadosEmpregador.FirstOrDefault(x => x.IdeEmpregador.NrInsc  == id);
        if (empregadorParaAtualizar != null)
        {
            DadosEmpregador.Remove(empregadorParaAtualizar);
        }
    }
}