using Ramos.eSocial.S1000.Application.Commands;
using Ramos.eSocial.S1000.Domain.Interfaces;
using Ramos.eSocial.S1000.Shared.Commands;
using Ramos.eSocial.S1000.Shared.Handlers;

namespace Ramos.eSocial.S1000.Application.Handlers;

public class FetchDadosEmpregadorCommandHandler : ICommandHandler<FetchDadosEmpregadorCommand>
{
    private readonly IDadosEmpregadorRepository _repository;
    

    public FetchDadosEmpregadorCommandHandler(IDadosEmpregadorRepository repository)
    {
        _repository = repository;
    }
    public async Task<ICommandResult> Handle(FetchDadosEmpregadorCommand command)
    {
       //fail fast validations
        if (string.IsNullOrEmpty(command.NrInsc))
        {
            return  new CommandResult(false, "Informar um nímero de inscrição válido.");;
        }
        
        //confirma a existência do Empregador cadastrado
        var empregador = await _repository.GetByIdAsync(command.NrInsc);
        if (empregador == null)
        {
            return  new CommandResult(false, "Empregador não cadastrado com este documento!");
        }
        
        //retornar informações
        return new CommandResult(empregador);
    }
}