using Ramos.eSocial.S1000.Shared.Commands;

namespace Ramos.eSocial.S1000.Application.Commands;

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
    public List<string> Message { get; private set; } = new List<string>();
}