using FluentValidation;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Domain.Validators;

public class InfoOrgInternacionalValidator : AbstractValidator<InfoOrgInternacional>
{
    public InfoOrgInternacionalValidator()
    {
        RuleFor(x => x.IndAcordoIsenMulta)
            .IsInEnum()
            .WithMessage("InfoOrgInternacional - Indicativo da existência de acordo internacional para isenção de multa, selecione um valor válido [0 ou 1]");
    }
}