using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Validators;
using Ramos.eSocial.Shared.Versao_S_1._3.Commands;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Base;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Commands;

public class CreateDadosEmpregadorCommand : Notifiable, ICommand 
{
    public IdeEmpregador IdeEmpregador { get; set; }
    public IdePeriodo IdePeriodo { get; set; }
    public InfoCadastro InfoCadastro { get; set; }
    public DadosIsencao DadosIsencao { get; set; }
    public InfoOrgInternacional InfoOrgInternacional { get; set; }
    
    public void Validate()
    {
        var dadosEmpregador = new DadosEmpregador(IdeEmpregador, IdePeriodo, InfoCadastro, DadosIsencao, InfoOrgInternacional);
        dadosEmpregador.Validate();
        IsValid = dadosEmpregador.IsValid;
        ValidationErrors.Clear();
        ValidationErrors.AddRange(dadosEmpregador.ValidationErrors);
    }
    
}