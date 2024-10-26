using Ramos.eSocial.S1000.Shared.Commands;
using Ramos.eSocial.S1000.Shared.Handlers;

namespace Ramos.eSocial.S1000.Application.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandResult Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
        return handler?.Handle(command);
    }

}