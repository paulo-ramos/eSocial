using FluentValidation;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Domain.Validators;

public class IdePeriodoValidator: AbstractValidator<IdePeriodo>
{
    public IdePeriodoValidator()
    {
        RuleFor(x => x.IniValid).NotEmpty().WithMessage("IdePeriodo - Início de Validade não pode ser vazio.");
        
        RuleFor(x => x.FimValid)
            .NotEmpty().WithMessage("IdePeriodo - Fim de Validade não pode ser vazio.")
            .GreaterThan(x => x.IniValid ).WithMessage("IdePeriodo - Fim de Validade não pode ser anterior ao Início de Validade.");
    }
}