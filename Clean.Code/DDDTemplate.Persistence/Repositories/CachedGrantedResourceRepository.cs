using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.Utils;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using DDDTemplate.SharedKernel.Abstractions.Services;

namespace DDDTemplate.Persistence.Repositories;

public class CachedGrantedResourceRepository(
    GrantedResourceRepository decorated,
    ICacheService cacheService,
    IDateTimeProvider dateTimeProvider) : IGrantedResourceRepository
{
    public Task<GrantedResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return decorated.GetByIdAsync(id, cancellationToken);
    }

    public Task<GrantedResource[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return decorated.GetByIdsAsync(ids, cancellationToken);
    }

    public Task<GrantedResource?> GetByRoleAndResourceAsync(UserRoles role, ResourceCode resourceCode,
        CancellationToken cancellationToken = default)
    {
        return cacheService.GetOrCreateAsync(
            CacheKeyUtil.GrantedResources.GetByRoleAndResource(role, resourceCode),
            () => decorated.GetByRoleAndResourceAsync(role, resourceCode, cancellationToken),
            dateTimeProvider.Now.AddMinutes(30), cancellationToken);
    }

    public Task<GrantedResource[]> GetByRoleAndIdResourcesAsync(UserRoles role, Guid[] idResources,
        CancellationToken cancellationToken = default)
    {
        return decorated.GetByRoleAndIdResourcesAsync(role, idResources, cancellationToken);
    }

    public void Insert(GrantedResource record)
    {
        decorated.Insert(record);
    }

    public void Update(GrantedResource record)
    {
        decorated.Update(record);
    }

    public void Remove(GrantedResource record)
    {
        decorated.Remove(record);
    }

    public void RemoveRange(IReadOnlyCollection<GrantedResource> records)
    {
        decorated.RemoveRange(records);
    }

    public void InsertRange(IReadOnlyCollection<GrantedResource> records)
    {
        decorated.InsertRange(records);
    }

    public void UpdateRange(IReadOnlyCollection<GrantedResource> records)
    {
        decorated.UpdateRange(records);
    }
}