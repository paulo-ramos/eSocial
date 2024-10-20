using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Commands;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Handlers;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Repositories;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Enums;
using Ramos.eSocial.Tests.Versao_S_1._3.S_1000.Repository;

namespace Ramos.eSocial.Tests.Versao_S_1._3.S_1000.Handlers;

public class CreateDadosEmpregadorHandlerTests
{
    [Fact]
    public void Handle_Should_Save_Valid_Command()
    {
        // Arrange
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

        var fakeRepository = new FakeDadosEmpregadorRepository();
        
        var handler = new CreateDadosEmpregadorHandler(fakeRepository);

        // Act
        handler.Handle(command);
        
        //Assert
        Assert.True(command.IsValid);
        Assert.Empty(command.ValidationErrors);
        Assert.Single(fakeRepository.DadosEmpregador);
        Assert.Equal("12345678901234", fakeRepository.DadosEmpregador[0].IdeEmpregador.NrInsc);
    }

    [Fact]
    public void Handle_Should_Not_Save_Invalid_Command()
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

        var fakeRepository = new FakeDadosEmpregadorRepository();
        
        var handler = new CreateDadosEmpregadorHandler(fakeRepository);

        // Act
        handler.Handle(command);

        // Assert
        Assert.False(command.IsValid);
        Assert.NotEmpty(command.ValidationErrors);
        Assert.Empty(fakeRepository.DadosEmpregador);

    }
}