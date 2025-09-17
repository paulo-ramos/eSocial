using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Abstractions.Providers;
using DDDTemplate.Application.Authentication.Common;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using DDDTemplate.SharedKernel.Abstractions.Services;
using DDDTemplate.SharedKernel.Results;

namespace DDDTemplate.Application.Authentication.Commands.Signin;

public class SigninCommandHandler(
    IUserRepository repository,
    IEmployeeRepository employeeRepository,
    IJwtProvider jwtProvider,
    IPasswordService passwordService,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<SigninCommand, SigninInfoResultModel>
{
    public async Task<Result<SigninInfoResultModel>> Handle(SigninCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user == null)
            return Result.Failure<SigninInfoResultModel>(UserErrors.IncorrectUsernameOrPassword);

        if (!passwordService.VerifyHashedPassword(Password.Format(request.Password), user.Password.Value))
            return Result.Failure<SigninInfoResultModel>(UserErrors.IncorrectUsernameOrPassword);

        if (user.ExpirationDate != null && user.ExpirationDate < dateTimeProvider.Now)
            return Result.Failure<SigninInfoResultModel>(UserErrors.AccountLockedByAdministrator);

        var employee = await employeeRepository.GetByIdUserAsync(user.Id, cancellationToken);
        if (employee == null)
            return Result.Failure<SigninInfoResultModel>(UserErrors.NotFound);

        return Result.Success(new SigninInfoResultModel(
            new SignedInUserModel(employee),
            await jwtProvider.GetAccessTokenAsync(user, request.DeviceId),
            await jwtProvider.GetRefreshTokenAsync(user, request.DeviceId))
        );
    }
}