using DDDTemplate.Application.Errors;
using FluentValidation;
using DDDTemplate.Application.Extensions;

namespace DDDTemplate.Application.Authentication.Commands.RenewAccessToken;

public class RenewAccessTokenCommandValidator : AbstractValidator<RenewAccessTokenCommand>
{
    public RenewAccessTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired);

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .NotEqual(Guid.Empty)
            .WithError(ValidationErrors.IsFormatWrong);

        RuleFor(x => x.DeviceId)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired);
    }
}