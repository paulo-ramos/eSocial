using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Domain.Entities;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Domain.ValueObjects;
using Ramos.eSocial.S1000.Shared.Commands;
using Ramos.eSocial.S1000.Shared.Handlers;

namespace Ramos.eSocial.S1000.Application.Handlers;

public class UpdateDadosEmpregadorCommandHandler : ICommandHandler<UpdateDadosEmpregadorCommand>
{
    
    private readonly IDadosEmpregadorRepository _repository;
    

    public UpdateDadosEmpregadorCommandHandler(IDadosEmpregadorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> Handle(UpdateDadosEmpregadorCommand command)
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
        
        //confirma a existência do Empregador cadastrado
        var empregadorExists = await _repository.ExistsAsync(dadosEmpregador.IdeEmpregador.NrInsc);
        if (!empregadorExists)
        {
            return  new CommandResult(false, "Empregador não cadastrado com este documento!");
        }
        
        // salvar as informações
        await _repository.UpdateAsync(dadosEmpregador.IdeEmpregador.NrInsc, dadosEmpregador);
        
        //retornar informações
        return new CommandResult(true, "Empregador atualizado com sucesso!");
    }
}