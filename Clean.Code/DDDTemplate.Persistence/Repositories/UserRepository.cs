using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public class UserRepository(IDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var record = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return record?.IsDeleted != false ? null : record;
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<User>()
            .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Username, username), cancellationToken);
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, Guid? id = null,
        CancellationToken cancellationToken = default)
    {
        var dbSet = DbContext.Set<User>();
        if (id == null || id == Guid.Empty)
            return !await dbSet.AnyAsync(x => !x.IsDeleted && EF.Functions.ILike(x.Username, username),
                cancellationToken: cancellationToken);

        return !await dbSet.AnyAsync(x => !x.IsDeleted && EF.Functions.ILike(x.Username, username) && x.Id != id,
            cancellationToken: cancellationToken);
    }

    public Task<User[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<User>().Where(x => !x.IsDeleted && ids.Contains(x.Id)).ToArrayAsync(cancellationToken);
    }
}