using FluentValidation;
using Ramos.eSocial.Shared.Domain.Enums;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class InfoCadastroValidator : AbstractValidator<InfoCadastro>
{
    public InfoCadastroValidator()
    {
        RuleFor(x => x.ClassTrib)
            .IsInEnum()
            .WithMessage("classTrib deve ser um código válido da Tabela 08.");

        RuleFor(x => x.IndDesFolha)
            .Must((obj, ind) =>
            {
                return ind switch
                {
                    EDesoneracaoFolha.EmpresaEnquadrada =>
                        obj.ClassTrib is EClassTributaria.SimplesNacional or EClassTributaria.EntidadeSemFinsLucrativos or EClassTributaria.Outros,

                    EDesoneracaoFolha.MunicipioEnquadrado =>
                        false, // Essa regra depende da natureza jurídica, que não está aqui

                    _ => true
                };
            })
            .WithMessage("indDesFolha inválido para a classificação tributária.");

        RuleFor(x => x.IndOpcCp)
            .Must((obj, ind) =>
            {
                if (ind == null) return true;
                return obj.ClassTrib is EClassTributaria.ProdutorRuralPJ or EClassTributaria.PessoaFisica;
            })
            .When(x => x.IndOpcCp != null)
            .WithMessage("indOpcCP só pode ser preenchido se classTrib for 07 ou 21.");

        RuleFor(x => x.IndPorte)
            .Must(p => p == null || p == "S")
            .WithMessage("indPorte deve ser 'S' ou nulo.");

        RuleFor(x => x.IndTribFolhaPisPasep)
            .Must(p => p == null || p == "S")
            .WithMessage("indTribFolhaPisPasep deve ser 'S' ou nulo.");

        RuleFor(x => x.IndPertIrrf)
            .Must(p => p == null || p == "S")
            .WithMessage("indPertIRRF deve ser 'S' ou nulo.");

        RuleFor(x => x.CnpjEfr)
            .Matches(@"^\d{14}$")
            .When(x => !string.IsNullOrEmpty(x.CnpjEfr))
            .WithMessage("CnpjEFR deve conter 14 dígitos numéricos.");

        RuleFor(x => x.DtTrans11096)
            .Must(date => date == null || date > new DateTime(2005, 11, 21))
            .WithMessage("dtTrans11096 deve ser posterior a 21/11/2005.");

        When(x => x.DadosIsencao != null, () =>
        {
            RuleFor(x => x.DadosIsencao).SetValidator(new DadosIsencaoValidator());
        });

        When(x => x.InfoOrgInternacional != null, () =>
        {
            RuleFor(x => x.InfoOrgInternacional).SetValidator(new InfoOrgInternacionalValidator());
        });
    }
}