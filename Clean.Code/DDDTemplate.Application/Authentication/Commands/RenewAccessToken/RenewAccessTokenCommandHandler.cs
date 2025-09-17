using DDDTemplate.Application.Abstractions.Messaging;
using DDDTemplate.Application.Abstractions.Providers;
using DDDTemplate.Application.Authentication.Common;
using DDDTemplate.Domain.Employees;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using DDDTemplate.SharedKernel.Results;

namespace DDDTemplate.Application.Authentication.Commands.RenewAccessToken;

public class RenewAccessTokenCommandHandler(
    IUserRepository repository,
    IEmployeeRepository employeeRepository,
    IJwtProvider jwtProvider,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<RenewAccessTokenCommand, SigninInfoResultModel>
{
    public async Task<Result<SigninInfoResultModel>> Handle(RenewAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        var userId = await jwtProvider.ValidateRefreshTokenAsync(request.RefreshToken, request.DeviceId);
        if (userId == null || userId != request.UserId)
            return Result.Failure<SigninInfoResultModel>(UserErrors.AccessDenied);

        var user = await repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            return Result.Failure<SigninInfoResultModel>(UserErrors.NotFound);

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