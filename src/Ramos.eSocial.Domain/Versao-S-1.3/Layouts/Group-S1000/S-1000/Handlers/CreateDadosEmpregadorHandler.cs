using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Commands;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Entities;
using Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Repositories;
using Ramos.eSocial.Shared.Versao_S_1._3.Commands;
using Ramos.eSocial.Shared.Versao_S_1._3.Domain.Entities;
using Ramos.eSocial.Shared.Versao_S_1._3.Handlers;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Handlers;

public class CreateDadosEmpregadorHandler : IHandler<CreateDadosEmpregadorCommand>
{

    private readonly IDadosEmpregadorRepository _repository;

    public CreateDadosEmpregadorHandler(IDadosEmpregadorRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateDadosEmpregadorCommand command)
    {
        //fail fast validations
        command.Validate();
        if (!command.IsValid)
        {
            return  new CommandResult(command.IsValid, command.ValidationErrors);;
        }
        //gerar as entidades auxiliares
        var ideEmpregador = new IdeEmpregador(command.IdeEmpregador.TpInsc, command.IdeEmpregador.NrInsc);
        var idePeriodo = new IdePeriodo(command.IdePeriodo.IniValid, command.IdePeriodo.FimValid);
        var infoCadastro = new InfoCadastro(command.InfoCadastro.ClassTrib, command.InfoCadastro.IndCoop, command.InfoCadastro.IndConstr, command.InfoCadastro.IndDesFolha, command.InfoCadastro.IndOpcCp, command.InfoCadastro.IndPorte, command.InfoCadastro.IndOptRegEletron, command.InfoCadastro.CnpjEfr, command.InfoCadastro.DtTrans11096, command.InfoCadastro.IndTribFolhaPisPasep);
        var dadosIsencao = new DadosIsencao(command.DadosIsencao.ideMinLei, command.DadosIsencao.nrCertif, command.DadosIsencao.dtEmisCertif, command.DadosIsencao.dtVencCertif, command.DadosIsencao.nrProtRenov, command.DadosIsencao.dtProtRenov, command.DadosIsencao.dtDou, command.DadosIsencao.pagDou);
        var infoOrgInternacional = new InfoOrgInternacional(command.InfoOrgInternacional.IndAcordoIsenMulta);
        
        // gerar a entidade principal
        var dadosEmpregador = new DadosEmpregador(ideEmpregador, idePeriodo, infoCadastro, dadosIsencao, infoOrgInternacional);
        
        // aplicar validação na entidade principal
        dadosEmpregador.Validate();
        if (!dadosEmpregador.IsValid)
        {
            return  new CommandResult(dadosEmpregador.IsValid, dadosEmpregador.ValidationErrors);
        }
        //verificar se ideEmpregador já está cadstrado
        var empregadorExists = _repository.DocumentExists(dadosEmpregador.IdeEmpregador.NrInsc);
        if (empregadorExists)
        {
            return  new CommandResult(false, "Empregador já existe com este documento!");
        }
        
        // salvar as informações
        _repository.SaveDadosEmpregador(dadosEmpregador);
        
        //retornar informações
        return new CommandResult(dadosEmpregador.IsValid, dadosEmpregador.ValidationErrors);
    }
}