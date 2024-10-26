using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Domain.Validators;

namespace Ramos.eSocial.S1000.Application.Validators;

public class CreateDadosEmpregadorCommandValidator : AbstractValidator<CreateDadosEmpregadorCommand>
{
    public CreateDadosEmpregadorCommandValidator()
    {
        RuleFor(x => x.IdeEmpregador)
            .SetValidator(new IdeEmpregadorValidator());

        RuleFor(x => x.IdePeriodo)
            .SetValidator(new IdePeriodoValidator());
        
        RuleFor(x => x.InfoCadastro)
            .SetValidator(new InfoCadastroValidator());
        
        RuleFor(x => x.DadosIsencao)
            .SetValidator(new DadosIsencaoValidator());
        
        RuleFor(x => x.InfoOrgInternacional)
            .SetValidator(new InfoOrgInternacionalValidator());  
    }
}