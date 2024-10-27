namespace Ramos.eSocial.S1000.Shared.Commands;

public interface ICommandResult
{
    object Data { get; }
    bool Success { get; }
    List<string> Message { get; }
}