using DDDTemplate.SharedKernel.Primitives;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Application.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<TEntity?> GetBydIdAsync<TEntity>(Guid id)
        where TEntity : Entity;

    void Insert<TEntity>(TEntity entity)
        where TEntity : Entity;

    void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : Entity;

    void Remove<TEntity>(TEntity entity)
        where TEntity : Entity;

    void RemoveRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : Entity;

    void Update<TEntity>(TEntity entity)
        where TEntity : Entity;

    void UpdateRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : Entity;
}