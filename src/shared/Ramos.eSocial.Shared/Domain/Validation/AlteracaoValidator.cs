using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class AlteracaoValidator : AbstractValidator<Alteracao?>
{
    public AlteracaoValidator()
    {
        RuleFor(x => x!).NotNull().WithMessage("Alteração não pode ser nula.");

        RuleFor(x => x!.IdePeriodo)
            .SetValidator(new IdePeriodoValidator());

        RuleFor(x => x!.InfoCadastro)
            .SetValidator(new InfoCadastroValidator());
    }
}