using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Application.Employees.Services;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Services;
using DDDTemplate.SharedKernel.Results;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler(
    IUnitOfWork unitOfWork,
    IEmployeeRepository repository,
    IPasswordService passwordService,
    IUserPermissionRepository userPermissionRepository,
    EmployeeService service,
    IIdentityService identityService
)
    : ICommandHandler<UpdateEmployeeCommand>
{
    public async Task<Result> Handle(UpdateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await service.ValidateAsync(request, cancellationToken);
        if (validate.IsFailure)
            return Result.Failure(validate.Error);

        var record = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (record == null)
            return Result.Failure(EmployeeErrors.NotFound);

        if (!identityService.ValidRole(record))
            return Result.Failure(EmployeeErrors.Unauthorized);

        if (!await repository.IsCodeUniqueAsync(request.Code, record.Id, cancellationToken))
            return Result.Failure(EmployeeErrors.DuplicateCode);

        var user = validate.Value.User;
        if (user != null)
        {
            user.Update(
                request.Username,
                Password.Create(request.Password, passwordService, true).Value,
                request.IsUserBlocked,
                request.ExpirationDate,
                request.UserRole ?? user.UserRole
            );

            var userPermissions = await userPermissionRepository.GetByIdUserAsync(user.Id, cancellationToken);
            if (userPermissions.Length > 0)
                userPermissionRepository.RemoveRange(userPermissions);

            userPermissions = validate.Value.UserPermissions;
            if (userPermissions.Length > 0)
                userPermissionRepository.InsertRange(userPermissions);
        }

        record.Update(
            request.Code,
            request.AvatarUrl,
            request.Fullname,
            EmailAddress.Create(request.EmailAddress).Value,
            MobileNumber.Create(request.MobileNumber).Value,
            request.Address,
            request.IsBlocked,
            user?.Id
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}