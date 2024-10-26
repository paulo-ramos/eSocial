using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Validators;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Enums;

namespace Ramos.eSocial.Tests.Versao_S_1._3.S_1000.Entities;

public class DadosEmpregadorTests
{
    private readonly EntityValidator<DadosEmpregador> validator;

    public DadosEmpregadorTests()
    {
        this.validator = new EntityValidator<DadosEmpregador>();
    }
    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var ideEmpregador = new IdeEmpregador(ETpInsc.CNPJ, "12345678901234");
        var idePeriodo = new IdePeriodo(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
        var infoCadastro = new InfoCadastro("12345678", 1, 1, 1, 1, "0", 1, "01.234.567/0001-90", DateTime.Now.AddYears(-1), "1");
        var dadosIsencao = new DadosIsencao("MF", "1234567890", DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1),
            "987654321", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-6), 500);
        var infoOrgInternacional = new InfoOrgInternacional(EIndAcordoIsenMulta.SemAcordo);
        
        var dadosEmpregador = new DadosEmpregador(ideEmpregador, idePeriodo, infoCadastro, dadosIsencao, infoOrgInternacional);
        
        dadosEmpregador.Validate();
        
        Assert.True(dadosEmpregador.IsValid);
        Assert.IsType<Guid>(dadosEmpregador.Id);
        Assert.True(DateTime.MinValue !=  dadosEmpregador.CreatedAt);
        Assert.Empty(dadosEmpregador.ValidationErrors);

    }
    
    
    [Fact]
    public void Should_Have_Error_When_NrINSC_Is_Empty()
    {
        var ideEmpregador = new IdeEmpregador(ETpInsc.CNPJ, "");
        var idePeriodo = new IdePeriodo(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
        var infoCadastro = new InfoCadastro("12345678", 1, 1, 1, 1, "0", 1, "01.234.567/0001-90", DateTime.Now.AddYears(-1), "1");
        var dadosIsencao = new DadosIsencao("MF", "1234567890", DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1),
            "987654321", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-6), 500);
        var infoOrgInternacional = new InfoOrgInternacional(EIndAcordoIsenMulta.SemAcordo);
        
        var dadosEmpregador = new DadosEmpregador(ideEmpregador, idePeriodo, infoCadastro, dadosIsencao, infoOrgInternacional);
        
        dadosEmpregador.Validate();
        
        Assert.False(dadosEmpregador.IsValid);
        Assert.IsType<Guid>(dadosEmpregador.Id);
        Assert.True(DateTime.MinValue !=  dadosEmpregador.CreatedAt);
        Assert.True(dadosEmpregador.ValidationErrors.Count == 1);

    }
}