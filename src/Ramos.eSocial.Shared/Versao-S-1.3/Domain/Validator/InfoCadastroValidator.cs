using FluentValidation;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

public class InfoCadastroValidator : AbstractValidator<InfoCadastro>
{
    public InfoCadastroValidator()
    {
        RuleFor(x => x.ClassTrib).NotNull().WithMessage("InfoCadastro - Classificação Tributária não pode ser vazio.");
        RuleFor(x => x.IndCoop).NotNull().WithMessage("InfoCadastro - Indicativo de cooperativa não pode ser vazio.");
        RuleFor(x => x.IndConstr).NotNull().WithMessage("InfoCadastro - Indicativo de construtora não pode ser vazio.");
        RuleFor(x => x.IndDesFolha).NotNull().WithMessage("InfoCadastro - Indicativo de opção/enquadramento de desoneração da folha não pode ser vazio.");
        RuleFor(x => x.IndOpcCp)
            .Null().When(x => x.ClassTrib is "07" or "21")
            .WithMessage(
                "InfoCadastro - Indicativo da opção pelo produtor rural deve ser vazia quando Classificação Tributária = [07 ou 21].")
            .NotNull().When(x => x.ClassTrib is not "07" and "21")
            .WithMessage(
                "InfoCadastro - Indicativo da opção pelo produtor rural deve ser fornecida quando Classificação Tributária diferente de [07 e 21].");
        RuleFor(x => x.IndPorte).NotNull().WithMessage("InfoCadastro - Indicativo de microempresa não pode ser vazio.");
        RuleFor(x => x.IndOptRegEletron).NotNull().WithMessage("InfoCadastro - Indicativo de opção pelo registro eletrônico de empregados não pode ser vazio.");
        RuleFor(x => x.CnpjEfr).NotNull().WithMessage("InfoCadastro - CNPJ do Ente Federativo Responsável não pode ser vazio.");
        RuleFor(x => x.DtTrans11096)
            .NotNull().When(x=>x.ClassTrib is not "21" and "22")
            .WithMessage("InfoCadastro - Data da transformação em sociedade de fins lucrativos deve ser fornecida quando Classificação Tributária diferente de [21 e 22].")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("InfoCadastro - Data da transformação em sociedade de fins lucrativos deve ser menor ou igual a data atual.")
            .GreaterThan(new DateTime(2005,11,21))
            .WithMessage("InfoCadastro - Data da transformação em sociedade de fins lucrativos deve ser maior que [2005 11 21].")
            .Null().When(x=>x.ClassTrib is "21" or "22")
            .WithMessage("InfoCadastro - Data da transformação em sociedade de fins lucrativos deve ser vazia quando Classificação Tributária = [21 ou 22].");
        RuleFor(x => x.IndTribFolhaPisPasep).NotNull().WithMessage("InfoCadastro - Indicador de tributação sobre a folha de pagamento - PIS e PASEP não pode ser vazio.");
    }
}