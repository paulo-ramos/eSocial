using DDDTemplate.Domain.UserPermissions;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public class UserPermissionRepository(DbContext dbContext) : IUserPermissionRepository
{
    public Task<UserPermission?> GetByIdUserAndIdResourceAsync(Guid idUser, Guid idResource,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Set<UserPermission>()
            .FirstOrDefaultAsync(x => x.IdUser == idUser && x.IdResource == idResource,
                cancellationToken: cancellationToken);
    }

    public Task<UserPermission[]> GetByIdUserAsync(Guid idUser, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<UserPermission>().Where(x => x.IdUser == idUser).ToArrayAsync(cancellationToken);
    }

    public void RemoveRange(IReadOnlyCollection<UserPermission> records)
    {
        dbContext.Set<UserPermission>().RemoveRange(records);
    }

    public void InsertRange(IReadOnlyCollection<UserPermission> records)
    {
        dbContext.Set<UserPermission>().AddRange(records);
    }
}