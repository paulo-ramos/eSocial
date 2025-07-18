using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class IdePeriodoValidator : AbstractValidator<IdePeriodo>
{
    public IdePeriodoValidator()
    {
        RuleFor(x => x.IniValid)
            .NotEqual(default(DateTime))
            .WithMessage("IniValid é obrigatório.");

        RuleFor(x => x.FimValid)
            .GreaterThanOrEqualTo(x => x.IniValid)
            .When(x => x.FimValid.HasValue)
            .WithMessage("FimValid deve ser igual ou posterior a IniValid.");
    }
}