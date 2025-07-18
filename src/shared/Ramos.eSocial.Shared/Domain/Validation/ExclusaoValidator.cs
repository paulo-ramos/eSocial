using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class ExclusaoValidator : AbstractValidator<Exclusao?>
{
    public ExclusaoValidator()
    {
        RuleFor(x => x!).NotNull().WithMessage("Exclusão não pode ser nula.");

        RuleFor(x => x!.IdePeriodo)
            .SetValidator(new IdePeriodoValidator());
    }
}