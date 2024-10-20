using FluentValidation;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

public class InfoOrgInternacionalValidator : AbstractValidator<InfoOrgInternacional>
{
    public InfoOrgInternacionalValidator()
    {
        RuleFor(x => x.IndAcordoIsenMulta)
            .IsInEnum()
            .WithMessage("InfoOrgInternacional - Indicativo da existência de acordo internacional para isenção de multa, selecione um valor válido [0 ou 1]");
    }
}