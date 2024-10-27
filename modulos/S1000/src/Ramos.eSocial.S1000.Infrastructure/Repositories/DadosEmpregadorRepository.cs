using MongoDB.Driver;
using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Infrastructure.Database;

namespace Ramos.eSocial.S1000.Infrastructure.Repositories;

public class DadosEmpregadorRepository: IDadosEmpregadorRepository
{
    private readonly IMongoCollection<DadosEmpregador> _mongoCollection;

    public DadosEmpregadorRepository(MongoDbContext dbContext)
    {
        _mongoCollection = dbContext.DadosEmpregador;
    }

    public async Task<List<DadosEmpregador>> GetAllAsync() => await _mongoCollection.Find(p => true).ToListAsync();

    public async Task<DadosEmpregador?> GetByIdAsync(string nrInsc)
    {
        var filter = Builders<DadosEmpregador>.Filter.Eq(p => p.IdeEmpregador.NrInsc, nrInsc);
        var bozo =  await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        return bozo;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var filter = Builders<DadosEmpregador>.Filter.Eq(p => p.IdeEmpregador.NrInsc, id);
        var bozo =  await _mongoCollection.Find(filter).AnyAsync();
        return bozo;
    }

    public async Task CreateAsync(DadosEmpregador dadosEmpregador) => await _mongoCollection.InsertOneAsync(dadosEmpregador);

    public async Task UpdateAsync(string id, DadosEmpregador dadosEmpregadorIn) => await _mongoCollection.ReplaceOneAsync(p => p.IdeEmpregador.NrInsc == id, dadosEmpregadorIn);

    public async Task DeleteAsync(string id) => await _mongoCollection.DeleteOneAsync(p => p.IdeEmpregador.NrInsc == id); 
}