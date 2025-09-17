using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Domain.Resources;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public class ResourceRepository(IDbContext dbContext) : GenericRepository<Resource>(dbContext), IResourceRepository
{
    public async Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var record = await DbContext.Set<Resource>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return record?.IsDeleted != false ? null : record;
    }

    public Task<Resource[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Resource>().Where(x => !x.IsDeleted && ids.Contains(x.Id)).ToArrayAsync(cancellationToken);
    }
}