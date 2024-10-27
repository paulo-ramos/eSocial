using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Domain.Validators;

namespace Ramos.eSocial.S1000.Application.Validators;

public class FetchDadosEmpregadorCommandValidator : AbstractValidator<FetchDadosEmpregadorCommand>
{
    public FetchDadosEmpregadorCommandValidator()
    {
        RuleFor(x => x.NrInsc).NotEmpty().WithMessage("Número de Inscrição não pode ser vazio."); 
    }
}