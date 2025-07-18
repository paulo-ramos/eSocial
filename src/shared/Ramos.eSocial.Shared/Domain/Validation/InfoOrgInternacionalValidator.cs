using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class InfoOrgInternacionalValidator : AbstractValidator<InfoOrgInternacional?>
{
    public InfoOrgInternacionalValidator()
    {
        RuleFor(x => x!).NotNull().WithMessage("infoOrgInternacional não pode ser nulo.");

        RuleFor(x => x!.IndAcordoIsenMulta)
            .IsInEnum()
            .WithMessage("indAcordoIsenMulta deve ser 0 (sem acordo) ou 1 (com acordo).");
    }
}