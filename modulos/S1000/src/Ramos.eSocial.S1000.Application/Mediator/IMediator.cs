using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Application.Mediator;

public interface IMediator
{
    ICommandResult Send<TCommand>(TCommand command) where TCommand : ICommand;
}