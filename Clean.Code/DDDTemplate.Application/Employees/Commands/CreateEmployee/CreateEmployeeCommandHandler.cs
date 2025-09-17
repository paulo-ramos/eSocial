using DDDTemplate.Application.Abstractions.Data;
using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Employees.Services;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Results;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler(
    IUnitOfWork unitOfWork,
    IEmployeeRepository repository,
    IUserRepository userRepository,
    IUserPermissionRepository userPermissionRepository,
    EmployeeService service)
    : ICommandHandler<CreateEmployeeCommand, CreateEmployeeCommandResult>
{
    public async Task<Result<CreateEmployeeCommandResult>> Handle(CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await service.ValidateAsync(request, cancellationToken);
        if (validate.IsFailure)
            return Result.Failure<CreateEmployeeCommandResult>(validate.Error);

        if(!await repository.IsCodeUniqueAsync(request.Code, null, cancellationToken))
            return Result.Failure<CreateEmployeeCommandResult>(EmployeeErrors.DuplicateCode);
        
        var user = validate.Value.User;
        if (user != null)
        {
            userRepository.Insert(user);

            var userPermissions = validate.Value.UserPermissions;
            if (userPermissions.Length > 0)
                userPermissionRepository.InsertRange(userPermissions);
        }
        
        var record = Employee.Create(
            request.Code,
            request.AvatarUrl,
            request.Fullname,
            EmailAddress.Create(request.EmailAddress).Value,
            MobileNumber.Create(request.MobileNumber).Value,
            request.Address,
            request.IsBlocked,
            user?.Id
        );

        repository.Insert(record);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateEmployeeCommandResult(record.Id));
    }
}