using DDDTemplate.Application.Employees.Abstractions;
using DDDTemplate.Application.Errors;
using FluentValidation;
using DDDTemplate.Application.Extensions;
using DDDTemplate.Domain.Users;
using DDDTemplate.SharedKernel.ValueObjects;

namespace DDDTemplate.Application.Employees.Common;

public class EmployeeValidator<TAuditable> : AbstractValidator<TAuditable> where TAuditable : IEmployeeAuditable
{
    protected EmployeeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .MaximumLength(50)
            .WithError(ValidationErrors.MaximumLength(50));

        RuleFor(x => x.Fullname)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .MaximumLength(200)
            .WithError(ValidationErrors.MaximumLength(200));

        RuleFor(x => x.EmailAddress)
            .MaximumLength(EmailAddress.MaxLength)
            .WithError(ValidationErrors.MaximumLength(EmailAddress.MaxLength));

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithError(ValidationErrors.IsRequired)
            .MaximumLength(MobileNumber.MaxLength)
            .WithError(ValidationErrors.MaximumLength(MobileNumber.MaxLength));

        RuleFor(x => x.Address)
            .MaximumLength(250)
            .WithError(ValidationErrors.MaximumLength(250));

        RuleFor(x => x.Username)
            .MaximumLength(50)
            .WithError(ValidationErrors.MaximumLength(50));

        RuleFor(x => x.Password)
            .MaximumLength(Password.MaxLength)
            .WithError(ValidationErrors.MaximumLength(Password.MaxLength));
        
        RuleForEach(x => x.IdResources)
            .NotEqual(Guid.Empty)
            .WithError(ValidationErrors.IsFormatWrong);
    }
}