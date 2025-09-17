using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace DDDTemplate.Persistence.Repositories;

public class EmployeeRepsitory(IDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
{
    public async Task<bool> IsCodeUniqueAsync(string code, Guid? id = null,
        CancellationToken cancellationToken = default)
    {
        var dbSet = DbContext.Set<Employee>();
        if (id == null || id == Guid.Empty)
            return !await dbSet.AnyAsync(x => !x.IsDeleted && EF.Functions.ILike(x.Code, code),
                cancellationToken);

        return !await dbSet.AnyAsync(x => !x.IsDeleted && EF.Functions.ILike(x.Code, code) && x.Id != id,
            cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var record = await DbContext.Set<Employee>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return record?.IsDeleted != false ? null : record;
    }

    public async Task<Employee?> GetByIdUserAsync(Guid idUser, CancellationToken cancellationToken = default)
    {
        var record = await DbContext.Set<Employee>().FirstOrDefaultAsync(x => x.IdUser == idUser, cancellationToken);
        return record?.IsDeleted != false ? null : record;
    }

    public Task<Employee[]> GetByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Employee>().Where(x => !x.IsDeleted && ids.Contains(x.Id)).ToArrayAsync(cancellationToken);
    }

    public IQueryable<Employee> GetQueryable(string keyword)
    {
        var queryable = DbContext.Set<Employee>().Where(x => !x.IsDeleted);
        if (!string.IsNullOrEmpty(keyword))
            queryable = queryable.Where(x =>
                EF.Functions
                    .ToTsVector($"{x.Code} {x.Fullname} {x.MobileNumber.Value} {x.EmailAddress.Value}")
                    .Matches(keyword));

        return queryable;
    }
}