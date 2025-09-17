using DDDTemplate.Application.Employees.Common;
using DDDTemplate.Application.Errors;
using FluentValidation;
using DDDTemplate.Application.Extensions;
using DDDTemplate.Domain.Users;

namespace DDDTemplate.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : EmployeeValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator(): base()
    {
        RuleFor(x => x.ConfirmPassword)
            .MaximumLength(Password.MaxLength)
            .WithError(ValidationErrors.MaximumLength(Password.MaxLength))
            .Must((auditable, s) => auditable.Password.Equals(s))
            .WithError(ValidationErrors.NotMach("password"));
    }
}