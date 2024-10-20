using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Validators;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Base;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;

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
        var validator = new EntityValidator<DadosEmpregador>();
        validator.AddValidationForDadosEmpregador();
        base.Validate(validator);
    }
}