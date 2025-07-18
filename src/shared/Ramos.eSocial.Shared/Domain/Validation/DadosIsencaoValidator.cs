using FluentValidation;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class DadosIsencaoValidator : AbstractValidator<DadosIsencao?>
{
    public DadosIsencaoValidator()
    {
        RuleFor(x => x!).NotNull().WithMessage("Dados de isenção não podem ser nulos.");

        RuleFor(x => x!.IdeMinLei)
            .NotEmpty().WithMessage("ideMinLei é obrigatório.");

        RuleFor(x => x!.NrCertif)
            .NotEmpty().WithMessage("nrCertif é obrigatório.");

        RuleFor(x => x!.DtEmisCertif)
            .NotEmpty().WithMessage("dtEmisCertif é obrigatório.");

        RuleFor(x => x!.DtVencCertif)
            .NotEmpty().WithMessage("dtVencCertif é obrigatório.")
            .GreaterThanOrEqualTo(x => x!.DtEmisCertif)
            .WithMessage("dtVencCertif não pode ser anterior a dtEmisCertif.");

        RuleFor(x => x!.PagDou)
            .InclusiveBetween(1, 99999)
            .When(x => x!.PagDou.HasValue)
            .WithMessage("pagDou deve estar entre 1 e 99999, se informado.");
    }
}