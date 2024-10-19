using FluentValidation;
using Ramos.eSocial.S_1000.Domain.Validator;
using Ramos.eSocial.Shared.Domain.Base;
using Ramos.eSocial.Shared.Domain.Entities;

namespace Ramos.eSocial.S_1000.Domain.Entities;

public class DadosEmpregador : Entity
{
    public DadosEmpregador(IdeEmpregador ideEmpregador, IdePeriodo idePeriodo, InfoCadastro infoCadastro, DadosIsencao dadosIsencao, InfoOrgInternacional infoOrgInternacional)
    {
        IdeEmpregador = ideEmpregador;
        IdePeriodo = idePeriodo;
        InfoCadastro = infoCadastro;
        DadosIsencao = dadosIsencao;
        InfoOrgInternacional = infoOrgInternacional;
    }

    public IdeEmpregador IdeEmpregador { get; private set; }
    public IdePeriodo IdePeriodo { get; private set; }
    public InfoCadastro InfoCadastro { get; private set; }
    public DadosIsencao DadosIsencao { get; private set; }
    public InfoOrgInternacional InfoOrgInternacional { get; private set; }
    
    public void Validate()
    {
        var validator = new DadosEmpregadorValidator();
        base.Validate(validator);
    }
}