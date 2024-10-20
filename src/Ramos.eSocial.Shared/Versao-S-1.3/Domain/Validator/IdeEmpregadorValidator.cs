using FluentValidation;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

public class IdeEmpregadorValidator: AbstractValidator<IdeEmpregador>
{
    public IdeEmpregadorValidator()
    {
        RuleFor(x => x.TpInsc).NotEmpty().WithMessage("IdeEmpregador - Tipo de Inscrição não pode ser vazio.");
        RuleFor(x => x.NrInsc).NotEmpty().WithMessage("IdeEmpregador - Número de Inscrição não pode ser vazio.");
    }
}