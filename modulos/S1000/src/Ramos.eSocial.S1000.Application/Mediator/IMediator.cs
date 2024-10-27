using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Application.Mediator;

public interface IMediator
{
    Task<ICommandResult> Send<TCommand>(TCommand command) where TCommand : ICommand;
}