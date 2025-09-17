using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDDTemplate.SharedKernel.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DDDTemplate.Persistence.Interceptors;

public class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
            await PublishDomainEvents(eventData.Context, cancellationToken);
        
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    
    private async Task PublishDomainEvents(Microsoft.EntityFrameworkCore.DbContext dbContext, CancellationToken cancellationToken = default)
    {
        var domainEvents = dbContext
            .ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entity => entity.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }
}