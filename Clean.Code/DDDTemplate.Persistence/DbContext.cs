using System.Reflection;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.SharedKernel.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace DDDTemplate.Persistence;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext, IUnitOfWork
{
    public DbContext()
    {
    }

    public DbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        // var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=103.42.58.52;Database=navi_smart_dev;Username=postgres;Password=sys112233");
        var dataSourceBuilder =
            new NpgsqlDataSourceBuilder(
                "Host=127.0.0.1;Database=jp_dev;Username=postgres;Password=sys112233"
            );
        dataSourceBuilder.EnableDynamicJson();
        dataSourceBuilder.UseJsonNet();
        var dataSource = dataSourceBuilder.Build();
        optionsBuilder.UseNpgsql(dataSource);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    
    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : class
        => base.Set<TEntity>();

    public Task<TEntity?> GetBydIdAsync<TEntity>(Guid id)
        where TEntity : Entity
        => Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

    public void Insert<TEntity>(TEntity entity)
        where TEntity : Entity
        => Set<TEntity>().Add(entity);

    public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : Entity
        => Set<TEntity>().AddRange(entities);

    public new void Remove<TEntity>(TEntity entity)
        where TEntity : Entity
        => Set<TEntity>().Remove(entity);

    public void RemoveRange<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : Entity
        => Set<TEntity>().RemoveRange(entities);

    public new void Update<TEntity>(TEntity entity) where TEntity : Entity
        => Set<TEntity>().Update(entity);

    public void UpdateRange<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : Entity
        => Set<TEntity>().UpdateRange(entities);

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    public void ClearChangeTracker()
        => ChangeTracker.Clear();
}