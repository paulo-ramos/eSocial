using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Persistence.Specifications;
using DDDTemplate.SharedKernel.Primitives;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public abstract class GenericRepository<TEntity>(IDbContext dbContext) where TEntity : Entity
{
    protected IDbContext DbContext { get; } = dbContext;

    public void Insert(TEntity entity) => DbContext.Insert(entity);

    public void InsertRange(IReadOnlyCollection<TEntity> entities) => DbContext.InsertRange(entities);

    public void RemoveRange(IReadOnlyCollection<TEntity> entities) => DbContext.RemoveRange(entities);

    public void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) => DbContext.Remove(entity);

    public void UpdateRange(IReadOnlyCollection<TEntity> entities) => DbContext.Set<TEntity>().UpdateRange(entities);

    protected Task<bool> AnyAsync(Specification<TEntity> specification)
        => DbContext.Set<TEntity>().AnyAsync(specification);

    protected Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification)
        => DbContext.Set<TEntity>().FirstOrDefaultAsync(specification);
}