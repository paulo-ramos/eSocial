using Moq;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.Shared.Domain.Enums;
using Ramos.eSocial.Shared.Domain.Models;
using Ramos.eSocial.Shared.Util.Interface;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;

namespace Ramos.eSocial.Tests.Application.Handlers;

public class EventoS1000InclusaoHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveIncluirEventoComIdGerado()
    {
        // Arrange
        var dto = new EventoS1000Dto
        {
            IdeEvento = new IdeEventoS1000Dto { TpAmb = EAmbiente.TesteInterno, ProcEmi = EProcEmissao.AplicativoEmpregador, VerProc = "1.0" },
            IdeEmpregador = new IdeEmpregadorDto { TpInsc = ETipoInscricao.CNPJ, NrInsc = "12345678000195" },
            Inclusao = new InclusaoDto
            {
                IdePeriodo = new IdePeriodoDto
                {
                    IniValid = new DateTime(2025, 07, 01),
                    FimValid = new DateTime(2025, 12, 01)
                },
                InfoCadastro = new InfoCadastroDto
                {
                    RazaoSocial = "Empresa XPTO",
                    NomeFantasia = "XPTO",
                    ClassTrib = EClassTributaria.LucroReal,
                    IndDesFolha = EDesoneracaoFolha.NaoAplicavel,
                    IndOptRegEletron = ERegistroEletronico.Optou
                }
            }
        };

        var comando = new IncluirEventoS1000Command(dto);

        var idGerado = "ID123456789";

        var idGenMock = new Mock<IEventIdGenerator>();
        idGenMock.Setup(x => x.Gerar(dto.IdeEmpregador.NrInsc, "S1000"))
            .Returns(idGerado);

        var repoMock = new Mock<IEventoS1000Repository>();
        repoMock.Setup(x => x.IncluirAsync(It.IsAny<EvtInfoEmpregador>()))
            .Returns(Task.CompletedTask);

        var handler = new IncluirEventoS1000Handler(repoMock.Object, idGenMock.Object);

        // Act
        var resultado = await handler.HandleAsync(comando);

        // Assert
        Assert.Equal(idGerado, resultado.Id);
        repoMock.Verify(x => x.IncluirAsync(It.Is<EvtInfoEmpregador>(e => e.Id == idGerado)), Times.Once);
    }
}