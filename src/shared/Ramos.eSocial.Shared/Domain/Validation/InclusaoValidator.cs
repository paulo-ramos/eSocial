using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class InclusaoValidator : AbstractValidator<Inclusao?>
{
    public InclusaoValidator()
    {
        RuleFor(x => x!).NotNull().WithMessage("Inclusão não pode ser nula.");

        RuleFor(x => x!.IdePeriodo)
            .SetValidator(new IdePeriodoValidator());

        RuleFor(x => x!.InfoCadastro)
            .SetValidator(new InfoCadastroValidator());
    }
}