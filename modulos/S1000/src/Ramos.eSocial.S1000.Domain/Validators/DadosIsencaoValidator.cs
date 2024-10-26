using FluentValidation;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Domain.Validators;

public class DadosIsencaoValidator : AbstractValidator<DadosIsencao>
{
    public DadosIsencaoValidator()
    {
        RuleFor(x => x.dtVencCertif)
            .GreaterThan(x => x.dtEmisCertif )
            .WithMessage("DadosIsencao - Data de Vencimento do Certificado não pode ser menor que a data da Emissão do Certificado.");
    }
}