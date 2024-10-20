using FluentValidation;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

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