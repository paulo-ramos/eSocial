using MongoDB.Driver;
using Ramos.eSocial.S1000.Domain.Entities;

namespace Ramos.eSocial.S1000.Infrastructure.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        _database = client.GetDatabase("esocial");
    }

    public IMongoCollection<DadosEmpregador> DadosEmpregador => _database.GetCollection<DadosEmpregador>("dados-empregador");
}
