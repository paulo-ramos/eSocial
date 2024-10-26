using FluentValidation;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Domain.Validators;

public class IdeEmpregadorValidator: AbstractValidator<IdeEmpregador>
{
    public IdeEmpregadorValidator()
    {
        RuleFor(x => x.TpInsc).NotEmpty().WithMessage("IdeEmpregador - Tipo de Inscrição não pode ser vazio.");
        RuleFor(x => x.NrInsc).NotEmpty().WithMessage("IdeEmpregador - Número de Inscrição não pode ser vazio.");
    }
}