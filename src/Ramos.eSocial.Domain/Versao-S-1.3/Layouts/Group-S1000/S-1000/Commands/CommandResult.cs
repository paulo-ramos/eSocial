using Ramos.eSocial.Shared.Versao_S_1._3.Commands;

namespace Ramos.eSocial.Domain.Versao_S_1._3.Layouts.Group_S1000.S_1000.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult()
    {
        Message = new List<string>();
    }
    
    public CommandResult(bool success, string message)
    {
        Success = success;
        Message.Add(message);
    }
    
    public CommandResult(bool success,  List<string> message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; private set; }
    public List<string> Message { get; private set; }
    
}