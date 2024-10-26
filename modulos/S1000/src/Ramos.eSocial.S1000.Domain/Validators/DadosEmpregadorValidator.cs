using FluentValidation;
using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Domain.Validators;

public class DadosEmpregadorValidator : AbstractValidator<DadosEmpregador>
{
    public DadosEmpregadorValidator()
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