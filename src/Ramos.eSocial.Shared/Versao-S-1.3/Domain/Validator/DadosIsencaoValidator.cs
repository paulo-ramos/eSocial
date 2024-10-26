using FluentValidation;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

public class DadosIsencaoValidator : AbstractValidator<DadosIsencao>
{
    public DadosIsencaoValidator()
    {
        RuleFor(x => x.dtVencCertif)
            .GreaterThan(x => x.dtEmisCertif )
            .WithMessage("DadosIsencao - Data de Vencimento do Certificado não pode ser menor que a data da Emissão do Certificado.");
    }
}