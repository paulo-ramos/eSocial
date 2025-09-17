using System.Linq.Expressions;
using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Application.Data;
using DDDTemplate.Domain.Employees;
using DDDTemplate.SharedKernel.Results;

namespace DDDTemplate.Application.Employees.Queries.GetEmployees;

public class GetEmployeesQueryHandler(IIdentityService identityService, IEmployeeRepository repository)
    : IQueryHandler<GetEmployeesQuery, PageList<GetEmployeesQueryResult>>
{
    public async Task<Result<PageList<GetEmployeesQueryResult>>> Handle(GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = identityService.ValidRole(repository.GetQueryable(request.Keyword));

        if (request.RequestedIds is not null && request.RequestedIds.Length > 0)
            queryable = queryable.Where(x => request.RequestedIds.Contains(x.Id));

        return Result.Success(await PageList<GetEmployeesQueryResult>.CreateAsync(
            queryable, entity =>
                new GetEmployeesQueryResult
                {
                    Fullname = entity.Fullname,
                    MobileNumber = entity.MobileNumber.Value,
                    Code = entity.Code,
                    AvatarUrl = entity.AvatarUrl,
                    IsBlocked = entity.IsBlocked,
                    Id = entity.Id,
                    Email = entity.EmailAddress.Value
                }, request, GetSortProperty(request)));
    }

    private static Expression<Func<Employee, object>> GetSortProperty(IPageListRequest request)
        => request.SortBy switch
        {
            "name" => entity => entity.Fullname,
            _ => entity => entity.CreatedDateUtc
        };
}