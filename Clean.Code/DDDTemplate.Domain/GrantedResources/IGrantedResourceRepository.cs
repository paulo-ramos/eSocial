using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.Resources;

namespace DDDTemplate.Domain.GrantedResources;

public interface IGrantedResourceRepository
{
    Task<GrantedResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<GrantedResource[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
    Task<GrantedResource?> GetByRoleAndResourceAsync(UserRoles role, ResourceCode resourceCode,
        CancellationToken cancellationToken = default);

    Task<GrantedResource[]> GetByRoleAndIdResourcesAsync(UserRoles role, Guid[] idResources,
        CancellationToken cancellationToken = default);
    
    void Insert(GrantedResource record);
    void Update(GrantedResource record);
    void Remove(GrantedResource record);
    void RemoveRange(IReadOnlyCollection<GrantedResource> records);
    void InsertRange(IReadOnlyCollection<GrantedResource> records);
    void UpdateRange(IReadOnlyCollection<GrantedResource> records);
}