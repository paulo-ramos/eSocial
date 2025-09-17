using DDDTemplate.Application.Errors;
using FluentValidation;
using DDDTemplate.Application.Extensions;
using DDDTemplate.Domain.Users;

namespace DDDTemplate.Application.Authentication.Commands.Signin;

public class SignInCommandValidator : AbstractValidator<SigninCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .MaximumLength(50)
            .WithError(ValidationErrors.MaximumLength(50));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .MaximumLength(Password.MaxLength)
            .WithError(ValidationErrors.MaximumLength(Password.MaxLength))
            .MinimumLength(Password.MinLength)
            .WithError(ValidationErrors.MaximumLength(Password.MinLength));

        RuleFor(x => x.DeviceId)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired);
    }
}