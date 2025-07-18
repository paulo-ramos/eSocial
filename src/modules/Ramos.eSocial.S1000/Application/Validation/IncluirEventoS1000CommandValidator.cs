namespace Ramos.eSocial.S1000.Application.Validation;

using FluentValidation;
using Ramos.eSocial.S1000.Application.Commands;

public class IncluirEventoS1000CommandValidator : AbstractValidator<IncluirEventoS1000Command>
{
    public IncluirEventoS1000CommandValidator()
    {
        RuleFor(x => x.EventoDto).NotNull();

        RuleFor(x => x.EventoDto.IdeEvento).NotNull();
        RuleFor(x => x.EventoDto.IdeEmpregador).NotNull();
        RuleFor(x => x.EventoDto.Inclusao).NotNull();

        RuleFor(x => x.EventoDto.Inclusao.IdePeriodo.IniValid)
            .NotEmpty().WithMessage("Data de início é obrigatória.");

        RuleFor(x => x.EventoDto.Inclusao.InfoCadastro.RazaoSocial)
            .NotEmpty().WithMessage("Razão Social é obrigatória.");

        RuleFor(x => x.EventoDto.Inclusao.InfoCadastro.NomeFantasia)
            .NotEmpty().WithMessage("Nome Fantasia é obrigatório.");
    }
}