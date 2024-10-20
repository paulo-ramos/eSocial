using Ramos.eSocial.Shared.Versao_S_1._3.Commands;

namespace Ramos.eSocial.Shared.Versao_S_1._3.Handlers;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}