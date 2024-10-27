using System.Text.Json.Serialization;
using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.ValueObjects;
using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Application.Commands;

public class CreateDadosEmpregadorCommand : ICommand
{
    public IdeEmpregador IdeEmpregador { get; set; }
    public IdePeriodo IdePeriodo { get; set; }
    public InfoCadastro InfoCadastro { get; set; }
    public DadosIsencao DadosIsencao { get; set; }
    public InfoOrgInternacional InfoOrgInternacional { get; set; }
    
    [JsonIgnore]
    public bool IsValid { get; set; }
    
    [JsonIgnore]
    public List<string> ValidationErrors { get; set; } = new List<string>();

    
    public void Validate()
    {
        var dadosEmpregador = new DadosEmpregador(IdeEmpregador, IdePeriodo, InfoCadastro, DadosIsencao, InfoOrgInternacional);
        dadosEmpregador.Validate();
        IsValid = dadosEmpregador.IsValid;
        ValidationErrors.Clear();
        ValidationErrors.AddRange(dadosEmpregador.ValidationErrors);
    }
}