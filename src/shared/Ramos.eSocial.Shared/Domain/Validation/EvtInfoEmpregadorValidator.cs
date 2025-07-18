using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class EvtInfoEmpregadorValidator : AbstractValidator<EvtInfoEmpregador>
{
    public EvtInfoEmpregadorValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.IdeEvento).SetValidator(new IdeEventoS1000Validator());
        RuleFor(x => x.IdeEmpregador).SetValidator(new IdeEmpregadorValidator());
        RuleFor(x => x.InfoEmpregador).SetValidator(new InfoEmpregadorValidator());
    }
}