using System.Text.RegularExpressions;
using FluentValidation;
using Ramos.eSocial.Shared.Domain.Enums;
using Ramos.eSocial.Shared.Domain.Models;

namespace Ramos.eSocial.Shared.Domain.Validation;

public class IdeEmpregadorValidator : AbstractValidator<IdeEmpregador>
{
    public IdeEmpregadorValidator()
    {
        RuleFor(x => x.NrInsc)
            .NotEmpty()
            .Must((obj, nrInsc) =>
            {
                return obj.TpInsc switch
                {
                    ETipoInscricao.CNPJ => Regex.IsMatch(nrInsc, @"^\d{8}(\d{6})?$"),
                    ETipoInscricao.CPF => Regex.IsMatch(nrInsc, @"^\d{11}$"),
                    _ => false
                };
            }).WithMessage("Número de inscrição inválido para o tipo informado.");
    }
}