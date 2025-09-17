using DDDTemplate.SharedKernel.Events;

namespace DDDTemplate.Domain.UserLogs;

public class PushUserLogDomainEvent(
    Guid idUser,
    string entity,
    string action,
    string content,
    object? newData,
    object[] parameters)
    : IDomainEvent
{
    public PushUserLogDomainEvent(string entity, string action, string content, object? newData, object[] parameters) :
        this(Guid.Empty, entity, action, content, newData, parameters)
    {
    }

    public Guid IdUser { get; } = idUser;
    public string Entity { get; } = entity;
    public string Action { get; } = action;
    public string Content { get; } = content;
    public object? NewData { get; } = newData;
    public object[] Parameters { get; } = parameters;
}