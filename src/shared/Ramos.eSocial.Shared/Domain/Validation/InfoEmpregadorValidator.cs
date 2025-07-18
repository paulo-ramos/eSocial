using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class InfoEmpregadorValidator : AbstractValidator<InfoEmpregador>
{
    public InfoEmpregadorValidator()
    {
        RuleFor(x => x)
            .Must(x =>
            {
                int count = 0;
                if (x.Inclusao != null) count++;
                if (x.Alteracao != null) count++;
                if (x.Exclusao != null) count++;
                return count == 1;
            })
            .WithMessage("Deve ser informado exatamente e soente um dos grupos: inclusao, alteracao ou exclusao.");

        When(x => x.Inclusao != null, () =>
        {
            RuleFor(x => x.Inclusao).SetValidator(new InclusaoValidator());
        });

        When(x => x.Alteracao != null, () =>
        {
            RuleFor(x => x.Alteracao).SetValidator(new AlteracaoValidator());
        });

        When(x => x.Exclusao != null, () =>
        {
            RuleFor(x => x.Exclusao).SetValidator(new ExclusaoValidator());
        });
    }
}