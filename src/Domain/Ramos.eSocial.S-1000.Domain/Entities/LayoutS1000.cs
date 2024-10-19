using FluentValidation;
using Ramos.eSocial.S_1000.Domain.Validator;
using Ramos.eSocial.Shared.Domain.Validator;

namespace Ramos.eSocial.S_1000.Domain.Entities;

public class LayoutS1000
{
    public DadosEmpregador DadosEmpregador { get; set; }

    public LayoutS1000(DadosEmpregador dadosEmpregador)
    {
        DadosEmpregador = dadosEmpregador;
    }

    public void Validar()
    {
        var validadorEmpregador = new DadosEmpregadorValidator();
        validadorEmpregador.ValidateAndThrow(DadosEmpregador);
    }

}