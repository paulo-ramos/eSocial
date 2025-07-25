using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class IdePeriodoValidator : AbstractValidator<IdePeriodo>
{
    public IdePeriodoValidator()
    {
        RuleFor(x => x.IniValid)
            .NotEqual(DateTime.MinValue.Date)
            .WithMessage("IniValid é obrigatório.");

        RuleFor(x => x.FimValid)
            .GreaterThan(x => x.IniValid.Date)
            .WithMessage("FimValid deve ser posterior a IniValid.");
    }
}