using FluentValidation;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Validators;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;

public class LayoutS1000
{
    public DadosEmpregador DadosEmpregador { get; set; }

    public LayoutS1000(DadosEmpregador dadosEmpregador)
    {
        DadosEmpregador = dadosEmpregador;
    }
}