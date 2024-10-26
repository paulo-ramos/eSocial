using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Domain.Enums;
using Ramos.eSocial.S1000.Domain.ValueObjects;

namespace Ramos.eSocial.S1000.Tests.Commands;

public class CreateDadosEmpregadorCommandTests
{
    [Fact]
    public void Should_Have_Error_When_NrINSC_Is_Empty()
    {
        // Arrange
        var ideEmpregador = new IdeEmpregador(ETpInsc.CNPJ, "");
        var idePeriodo = new IdePeriodo(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));
        var infoCadastro = new InfoCadastro("12345678", 1, 1, 1, 1, "0", 1, "01.234.567/0001-90", DateTime.Now.AddYears(-1), "1");
        var dadosIsencao = new DadosIsencao("MF", "1234567890", DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1),
            "987654321", DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-6), 500);
        var infoOrgInternacional = new InfoOrgInternacional(EIndAcordoIsenMulta.SemAcordo);
        
        var command = new CreateDadosEmpregadorCommand
        {
            IdeEmpregador=ideEmpregador, 
            IdePeriodo = idePeriodo, 
            InfoCadastro = infoCadastro, 
            DadosIsencao = dadosIsencao, 
            InfoOrgInternacional = infoOrgInternacional
        };
        
        // Act
        command.Validate();

        // Assert
        Assert.False(command.IsValid);
        Assert.True(DateTime.MinValue !=  command.IdePeriodo.IniValid);
        Assert.NotEmpty(command.ValidationErrors);
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
        
        var command = new CreateDadosEmpregadorCommand
        {
            IdeEmpregador=ideEmpregador, 
            IdePeriodo = idePeriodo, 
            InfoCadastro = infoCadastro, 
            DadosIsencao = dadosIsencao, 
            InfoOrgInternacional = infoOrgInternacional
        };
        // Act
        command.Validate();

        // Assert
        Assert.True(command.IsValid);
        Assert.True(DateTime.MinValue !=  command.IdePeriodo.IniValid);
        Assert.Empty(command.ValidationErrors);
    }
}