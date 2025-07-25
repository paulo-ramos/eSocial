using FluentValidation;
using Moq;
using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Application.Dtos;
using Ramos.eSocial.S1000.Application.Handlers;
using Ramos.eSocial.Shared.Domain.Enums;
using Ramos.eSocial.Shared.Domain.Models;
using Ramos.eSocial.Shared.Util.Interface;
using Ramos.eSocial.S1000.Infrastructure.Repositories.Inteface;
using Ramos.eSocial.Shared.Util;

namespace Ramos.eSocial.Tests.Application.Handlers;

public class EventoS1000InclusaoHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveIncluirEventoComIdGerado()
    {
        // Arrange
        var numeroInscricao = "12345678000195";
        var codigoEvento = "S1000";
        var generator = new EventIdGenerator();
        
        var dto = new EventoS1000InclusaoDto
        {
            IdeEvento = new IdeEventoS1000Dto { TpAmb = EAmbiente.TesteInterno, ProcEmi = EProcEmissao.AplicativoEmpregador, VerProc = "1.0" },
            IdeEmpregador = new IdeEmpregadorDto { TpInsc = ETipoInscricao.CNPJ, NrInsc = numeroInscricao },
            Inclusao = new InclusaoDto
            {
                IdePeriodo = new IdePeriodoDto
                {
                    IniValid = new DateTime(2025, 07, 01).Date
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

        var idGerado = generator.Gerar(numeroInscricao, codigoEvento);
        var idGen = new EventIdGenerator();

        var repoMock = new Mock<IEventoS1000Repository>();
        repoMock.Setup(x => x.IncluirAsync(It.IsAny<EvtInfoEmpregador>()))
            .Returns(Task.CompletedTask);

        var handler = new IncluirEventoS1000Handler(repoMock.Object, idGen);

        // Act
        var resultado = await handler.HandleAsync(comando);

        // Assert
        Assert.Equal(
            idGerado.Substring(0, 30),
            resultado.Id.Substring(0, 30)
        );
        repoMock.Verify(x => x.IncluirAsync(It.Is<EvtInfoEmpregador>(e => e.Id == resultado.Id)), Times.Once);
    }
    
    [Fact]
    public async Task HandleAsync_DeveRetornarDtoComId_QuandoInclusaoValida()
    {
        // Arrange
        var dto = new EventoS1000InclusaoDto {
            IdeEvento = new IdeEventoS1000Dto
            {
                TpAmb = EAmbiente.TesteInterno,
                ProcEmi = EProcEmissao.AplicativoEmpregador,
                VerProc = "1.0"
            },
            IdeEmpregador = new IdeEmpregadorDto
            {
                TpInsc = ETipoInscricao.CNPJ,
                NrInsc = "12345678000100"
            },
            Inclusao = new InclusaoDto
            {
                IdePeriodo = new IdePeriodoDto
                {
                    IniValid = new DateTime(2025, 07, 01).Date
                },
                InfoCadastro = new InfoCadastroDto
                {
                    RazaoSocial = "Empresa Exemplo Ltda.",
                    NomeFantasia = "ExemploTech",
                    ClassTrib = EClassTributaria.ProdutorRuralPJ,
                    IndCoop = EIndicativoCooperativa.NaoCooperativa,
                    IndConstr = EIndicativoConstrutora.NaoConstrutora,
                    IndDesFolha = EDesoneracaoFolha.NaoAplicavel,
                    IndOpcCP = EOpcaoCP.FolhaPagamento,
                    IndPorte = null,
                    IndOptRegEletron = ERegistroEletronico.Optou,
                    CnpjEFR = null,
                    DtTrans11096 = null,
                    IndTribFolhaPisPasep  = null,
                    IndPertIRRF = null,
                    DadosIsencao = new DadosIsencaoDto 
                    {
                        IdeMinLei = "CRM",
                        NrCertif = "123456789",
                        DtEmisCertif = DateTime.Today,
                        DtVencCertif = DateTime.Today.AddYears(7),
                        NrProtRenov = null,
                        DtProtRenov = null,
                        DtDou = null,
                        PagDou = null
                    },
                    InfoOrgInternacional = new InfoOrgInternacionalDto
                    {
                        IndAcordoIsenMulta = EAcordoIsencaoMulta.SemAcordo
                    }
                }
            }
        };

        var comando = new IncluirEventoS1000Command(dto);

        var repositorioMock = new Mock<IEventoS1000Repository>();
        var idGeneratorMock = new Mock<IEventIdGenerator>();
        idGeneratorMock.Setup(x => x.Gerar(It.IsAny<string>(), "S1000")).Returns("ID123");

        var handler = new IncluirEventoS1000Handler(repositorioMock.Object, idGeneratorMock.Object);

        // Act
        var resultado = await handler.HandleAsync(comando);

        // Assert
        Assert.Equal("ID123", resultado.Id);
        repositorioMock.Verify(x => x.IncluirAsync(It.IsAny<EvtInfoEmpregador>()), Times.Once);
    }
    
    [Fact]
    public async Task HandleAsync_DeveLancarArgumentException_QuandoDtoInvalido()
    {
        var dto = new EventoS1000InclusaoDto
        {
            IdeEvento = null,
            IdeEmpregador = null,
            Inclusao = null
        };

        var comando = new IncluirEventoS1000Command(dto);
        var handler = new IncluirEventoS1000Handler(Mock.Of<IEventoS1000Repository>(), Mock.Of<IEventIdGenerator>());

        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(comando));
    }
    
    
    [Fact]
    public async Task HandleAsync_DeveLancarValidationException_QuandoDominioInvalido()
    {
        var dto = new EventoS1000InclusaoDto
        {
            IdeEvento = new IdeEventoS1000Dto()
            {
                TpAmb = EAmbiente.Desenvolvimento,
                ProcEmi = EProcEmissao.AplicativoEmpregador,
                VerProc = "1.0"
            },
            IdeEmpregador = new IdeEmpregadorDto()
            {
                NrInsc = "", 
                TpInsc = ETipoInscricao.CNPJ
            },
            Inclusao = new InclusaoDto()
            {
                IdePeriodo = new IdePeriodoDto()
            }
        };

        var comando = new IncluirEventoS1000Command(dto);

        var repositorioMock = new Mock<IEventoS1000Repository>();
        var idGeneratorMock = new Mock<IEventIdGenerator>();
        idGeneratorMock.Setup(x => x.Gerar(It.IsAny<string>(), "S1000")).Returns("ID123");

        var handler = new IncluirEventoS1000Handler(repositorioMock.Object, idGeneratorMock.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(comando));

        Assert.Contains("Dados obrigatórios ausentes.", exception.Message); 
    }
    
    
    [Fact]
    public async Task HandleAsync_DeveLancarValidationException_QuandoValidacaoDeDominioFalha()
    {
        var dto = new EventoS1000InclusaoDto
        {
            IdeEvento = new IdeEventoS1000Dto
            {
                TpAmb = 0, 
                ProcEmi = EProcEmissao.AplicativoEmpregador,
                VerProc = "1.0"
            },
            IdeEmpregador = new IdeEmpregadorDto
            {
                NrInsc = "12345678000199",
                TpInsc = ETipoInscricao.CNPJ
            },
            Inclusao = new InclusaoDto
            {
                IdePeriodo = new IdePeriodoDto
                {
                    IniValid = new DateTime(2025, 9, 1).Date,
                    FimValid = new DateTime(2025, 12, 31).Date
                },
                InfoCadastro = new InfoCadastroDto
                {
                    RazaoSocial = "Empresa Teste",
                    NomeFantasia = "Teste Ltda",
                    ClassTrib = 0
                }
            }
        };

        var comando = new IncluirEventoS1000Command(dto);

        var repositorioMock = new Mock<IEventoS1000Repository>();
        var idGeneratorMock = new Mock<IEventIdGenerator>();
        idGeneratorMock.Setup(x => x.Gerar(It.IsAny<string>(), "S1000")).Returns("ID123");

        var handler = new IncluirEventoS1000Handler(repositorioMock.Object, idGeneratorMock.Object);

        var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.HandleAsync(comando));

        Assert.Contains("classTrib deve ser um código válido da Tabela 08.", exception.Message);
    }
    
    
    
    [Fact]
    public async Task Handler_DeveRecusarInclusao_QuandoCnpjJaExiste()
    {
        // Arrange
        var cnpj = "12345678000100";
        var idExistente = "S1000-12345678000100-202507";
    
        var eventoExistente = EvtInfoEmpregador.CriarInclusao(
            idExistente,
            new IdeEventoS1000(EAmbiente.Desenvolvimento, EProcEmissao.AplicativoEmpregador, "1.0"),
            new IdeEmpregador(ETipoInscricao.CNPJ, cnpj),
            new Inclusao
            {
                IdePeriodo = new IdePeriodo(new DateTime(2025, 7, 1).Date, DateTime.MaxValue.Date),
                InfoCadastro = new InfoCadastro("Empresa X", "Fantasia", EClassTributaria.Outros, 0, 0, EDesoneracaoFolha.NaoAplicavel, EOpcaoCP.FolhaPagamento, "S", ERegistroEletronico.Optou, "11222333000199", new DateTime(2025, 1, 1), "S", "S", null, null)
            }
        );
    
        var repositorioMock = new Mock<IEventoS1000Repository>();
        repositorioMock
            .Setup(r => r.ObterPorNrInscAsync(cnpj))
            .ReturnsAsync(eventoExistente);
    
        var idGeneratorMock = new Mock<IEventIdGenerator>();
    
        var handler = new IncluirEventoS1000Handler(repositorioMock.Object, idGeneratorMock.Object);
    
        var dto = new EventoS1000InclusaoDto
        {
            IdeEvento = new IdeEventoS1000Dto { TpAmb = EAmbiente.Producao, ProcEmi = EProcEmissao.AplicativoEmpregador, VerProc = "1.0" },
            IdeEmpregador = new IdeEmpregadorDto { TpInsc = ETipoInscricao.CNPJ, NrInsc = cnpj },
            Inclusao = new InclusaoDto
            {
                IdePeriodo = new IdePeriodoDto { IniValid = new DateTime(2025,7,1).Date },
                InfoCadastro = new InfoCadastroDto { RazaoSocial = "Empresa X", ClassTrib = EClassTributaria.Outros }
            }
        };
    
        var comando = new IncluirEventoS1000Command(dto);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() => handler.HandleAsync(comando));
        Assert.Contains(idExistente, ex.Message);
    }
    
}