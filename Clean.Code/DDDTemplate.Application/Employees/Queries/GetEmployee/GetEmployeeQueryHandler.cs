using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Results;

namespace DDDTemplate.Application.Employees.Queries.GetEmployee;

public class GetEmployeeQueryHandler(
    IEmployeeRepository repository,
    IIdentityService identityService,
    IUserRepository userRepository,
    IUserPermissionRepository userPermissionRepository
)
    : IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResult>
{
    public async Task<Result<GetEmployeeQueryResult>> Handle(GetEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var record = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (record == null)
            return Result.Failure<GetEmployeeQueryResult>(EmployeeErrors.NotFound);

        if (!identityService.ValidRole(record))
            return Result.Failure<GetEmployeeQueryResult>(EmployeeErrors.Unauthorized);

        User? user = null;
        var userPermissions = Array.Empty<UserPermission>();
        if (record.IdUser != null)
        {
            user = await userRepository.GetByIdAsync((Guid)record.IdUser, cancellationToken);
            if (user != null)
                userPermissions = await userPermissionRepository.GetByIdUserAsync(user.Id, cancellationToken);
        }

        return Result.Success(new GetEmployeeQueryResult
        {
            Address = record.Address,
            Code = record.Code,
            Fullname = record.Fullname,
            Id = record.Id,
            IdUser = record.IdUser,
            Username = user?.Username ?? string.Empty,
            UserRole = user?.UserRole,
            ExpirationDate = user?.ExpirationDate,
            AvatarUrl = record.AvatarUrl,
            EmailAddress = record.EmailAddress.Value,
            MobileNumber = record.MobileNumber.Value,
            IdResources = userPermissions.Select(x => x.IdResource).ToArray(),
            IsBlocked = record.IsBlocked,
            IsUserBlocked = user?.IsBlocked ?? false
        });
    }
}