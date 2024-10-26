using FluentValidation;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Commands;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Validator;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Validators;

public class EntityValidator<T> : AbstractValidator<T> where T : class
{
    public void AddValidationForDadosEmpregador()
    {
        RuleFor(x => ((DadosEmpregador)(object)x).IdeEmpregador)
            .SetValidator(new IdeEmpregadorValidator());

        RuleFor(x => ((DadosEmpregador)(object)x).IdePeriodo)
            .SetValidator(new IdePeriodoValidator());
        
        RuleFor(x => ((DadosEmpregador)(object)x).InfoCadastro)
            .SetValidator(new InfoCadastroValidator());
        
        RuleFor(x => ((DadosEmpregador)(object)x).DadosIsencao)
            .SetValidator(new DadosIsencaoValidator());
        
        RuleFor(x => ((DadosEmpregador)(object)x).InfoOrgInternacional)
            .SetValidator(new InfoOrgInternacionalValidator());
    }
}