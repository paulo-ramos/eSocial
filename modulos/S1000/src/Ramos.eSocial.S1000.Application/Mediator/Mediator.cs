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

    public async Task<ICommandResult> Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
        return await handler?.Handle(command)!;
    }

}