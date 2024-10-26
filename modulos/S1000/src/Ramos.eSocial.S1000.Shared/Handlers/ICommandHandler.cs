﻿using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Shared.Handlers;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<ICommandResult> Handle(TCommand command);
}
