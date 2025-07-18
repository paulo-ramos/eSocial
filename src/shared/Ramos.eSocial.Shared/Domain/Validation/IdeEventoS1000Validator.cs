using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class IdeEventoS1000Validator : AbstractValidator<IdeEventoS1000>
{
    public IdeEventoS1000Validator()
    {
        RuleFor(x => x.VerProc)
            .NotEmpty()
            .MaximumLength(20);
    }
}