using MongoDB.Bson.Serialization.Attributes;
using Ramos.eSocial.S1000.Domain.Validators;
using Ramos.eSocial.S1000.Domain.ValueObjects;
using Ramos.eSocial.S1000.Shared.Entities.Base;

namespace Ramos.eSocial.S1000.Domain.Entities;

[BsonIgnoreExtraElements]
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