using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;

namespace Ramos.eSocial.S1000.Application.Validation;

public class AlterarEventoS1000CommandValidator : AbstractValidator<AlterarEventoS1000Command>
{
    public AlterarEventoS1000CommandValidator()
    {
        RuleFor(x => x.EventoDto).NotNull();

        RuleFor(x => x.EventoDto.IdeEvento).NotNull();
        RuleFor(x => x.EventoDto.IdeEmpregador).NotNull();
        RuleFor(x => x.EventoDto.Alteracao).NotNull();

        RuleFor(x => x.EventoDto.Alteracao.IdePeriodo.IniValid)
            .NotEmpty().WithMessage("Data de início é obrigatória.");

        RuleFor(x => x.EventoDto.Alteracao.InfoCadastro.RazaoSocial)
            .NotEmpty().WithMessage("Razão Social é obrigatória.");

        RuleFor(x => x.EventoDto.Alteracao.InfoCadastro.NomeFantasia)
            .NotEmpty().WithMessage("Nome Fantasia é obrigatório.");
    }


}