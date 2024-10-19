using Ramos.eSocial.S_1000.Domain.Entities;
using Ramos.eSocial.S_1000.Domain.Validator;
using Ramos.eSocial.Shared.Domain.Entities;
using Ramos.eSocial.Shared.Domain.Enums;

namespace Ramos.eSocial.Tests.S1000.Entities;

public class DadosEmpregadorTests
{
    private readonly DadosEmpregadorValidator validator;

    public DadosEmpregadorTests()
    {
        this.validator = new DadosEmpregadorValidator();
    }
    [Fact]
    public void DadosEmpregador_Quando_Tudo_Preenchido_Corretamente_Deve_Retornar_TRUE()
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
    public void DadosEmpregador_Quando_Faltando_CNPJ_Deve_Retornar_FALSE()
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