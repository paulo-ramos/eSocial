using DDDTemplate.SharedKernel.Events;
using Newtonsoft.Json;

namespace DDDTemplate.SharedKernel.Primitives;

public class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public AggregateRoot()
    {
    }

    public AggregateRoot(Guid id) : base(id)
    {
    }

    [JsonIgnore] public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}