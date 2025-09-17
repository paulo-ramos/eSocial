using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Application.Employees.Abstractions;
using DDDTemplate.Application.Employees.Common;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.Enums;
using DDDTemplate.Domain.GrantedResources;
using DDDTemplate.Domain.Resources;
using DDDTemplate.Domain.UserPermissions;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Services;
using DDDTemplate.SharedKernel.Results;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Application.Employees.Services;

public class EmployeeService(
    IUserRepository userRepository,
    IPasswordService passwordService,
    IResourceRepository resourceRepository,
    IGrantedResourceRepository grantedResourceRepository,
    IIdentityService identityService,
    IUserPermissionRepository userPermissionRepository
)
{
    public async Task<Result<EmployeeValidateResultModel>> ValidateAsync(IEmployeeAuditable request,
        CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(request.EmailAddress))
        {
            var result = EmailAddress.Create(request.EmailAddress);
            if (result.IsFailure)
                return Result.Failure<EmployeeValidateResultModel>(result.Error);
        }

        if (!string.IsNullOrEmpty(request.MobileNumber))
        {
            var result = MobileNumber.Create(request.MobileNumber);
            if (result.IsFailure)
                return Result.Failure<EmployeeValidateResultModel>(result.Error);
        }

        var resources = Array.Empty<Resource>();
        User? user = null;
        var userPermissions = Array.Empty<UserPermission>();

        if (request.IdUser != null)
        {
            user = await userRepository.GetByIdAsync((Guid)request.IdUser, cancellationToken);
            if (user == null)
                return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.UserInfoIsRequired);

            userPermissions = await userPermissionRepository.GetByIdUserAsync(user.Id, cancellationToken);
        }
        else
        {
            var isCreateUser = false;
            if (string.IsNullOrEmpty(request.Username))
            {
                if (!await userRepository.IsUsernameUniqueAsync(request.Username, cancellationToken: cancellationToken))
                    return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.DuplicateUsername);

                isCreateUser = true;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordResult = Password.Create(request.Password, passwordService, true);
                if (passwordResult.IsFailure)
                    return Result.Failure<EmployeeValidateResultModel>(passwordResult.Error);

                isCreateUser = true;
            }

            if (isCreateUser)
            {
                if (request.UserRole == null)
                    return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.UserInfoIsRequired);

                var userRoles = Enum.GetValues(typeof(UserRoles)).Cast<UserRoles>().ToArray();
                if (userRoles.All(x => x != request.UserRole))
                    return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.UserRoleWrongFormat);

                var idUser = Guid.NewGuid();

                user = User.Create(
                    idUser,
                    request.Username,
                    Password.Create(request.Password, passwordService, true).Value,
                    request.IsUserBlocked,
                    request.ExpirationDate,
                    (UserRoles)request.UserRole
                );
            }
        }

        if (request.UserRole != null)
        {
            switch (identityService.Role)
            {
                case UserRoles.Manager when request.UserRole == UserRoles.Administrator:
                    return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.Unauthorized);
                case UserRoles.Member when request.UserRole != UserRoles.Member:
                    return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.Unauthorized);
            }
        }

        if (user != null && request.UserRole is UserRoles.Manager or UserRoles.Member)
        {
            resources = await resourceRepository.GetByIdsAsync(request.IdResources, cancellationToken);
            if (resources.Length == 0)
                return Result.Failure<EmployeeValidateResultModel>(EmployeeErrors.ResourcesNotFound);

            var grantedResources =
                await grantedResourceRepository.GetByRoleAndIdResourcesAsync((UserRoles)request.UserRole,
                    resources.Select(x => x.Id).ToArray(),
                    cancellationToken);

            if (grantedResources.Length > 0)
            {
                userPermissions = grantedResources.Select(x => new UserPermission(
                    user.Id,
                    x.Id,
                    x.IdResource
                )).ToArray();
            }
        }

        return Result.Success(new EmployeeValidateResultModel(resources, user, userPermissions));
    }
}