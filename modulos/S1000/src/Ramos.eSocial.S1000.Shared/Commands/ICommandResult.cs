namespace Ramos.eSocial.S1000.Shared.Commands;

public interface ICommandResult
{
    bool Success { get; }
    List<string> Message { get; }
}