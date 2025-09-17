using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public class GrantedResourceRepository(IDbContext dbContext) : IGrantedResourceRepository
{
    public async Task<GrantedResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var record = await dbContext.Set<GrantedResource>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return record?.IsDeleted != false ? null : record;
    }

    public Task<GrantedResource[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<GrantedResource>().Where(x => !x.IsDeleted && ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken);
    }

    public Task<GrantedResource?> GetByRoleAndResourceAsync(UserRoles role, ResourceCode resourceCode,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Set<GrantedResource>().FirstOrDefaultAsync(x =>
            x.Role == role && !x.Resource.IsDeleted && !x.Resource.IsBlocked &&
            x.Resource.Code.Value == resourceCode.Value, cancellationToken);
    }

    public Task<GrantedResource[]> GetByRoleAndIdResourcesAsync(UserRoles role, Guid[] idResources,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Set<GrantedResource>().Where(x =>
                x.Role == role
                && idResources.Contains(x.IdResource))
            .ToArrayAsync(cancellationToken: cancellationToken);
    }

    public void Insert(GrantedResource record)
    {
        dbContext.Set<GrantedResource>().Add(record);
    }

    public void Update(GrantedResource record)
    {
        dbContext.Set<GrantedResource>().Update(record);
    }

    public void Remove(GrantedResource record)
    {
        dbContext.Set<GrantedResource>().Remove(record);
    }

    public void RemoveRange(IReadOnlyCollection<GrantedResource> records)
    {
        dbContext.Set<GrantedResource>().RemoveRange(records);
    }

    public void InsertRange(IReadOnlyCollection<GrantedResource> records)
    {
        dbContext.Set<GrantedResource>().AddRange(records);
    }

    public void UpdateRange(IReadOnlyCollection<GrantedResource> records)
    {
        dbContext.Set<GrantedResource>().UpdateRange(records);
    }
}