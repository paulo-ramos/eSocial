namespace DDDTemplate.Domain.UserPermissions;

public interface IUserPermissionRepository
{
    Task<UserPermission?> GetByIdUserAndIdResourceAsync(Guid idUser, Guid idResource, CancellationToken cancellationToken = default);
    Task<UserPermission[]> GetByIdUserAsync(Guid idUser, CancellationToken cancellationToken = default);
    
    void RemoveRange(IReadOnlyCollection<UserPermission> records);
    void InsertRange(IReadOnlyCollection<UserPermission> records);
}