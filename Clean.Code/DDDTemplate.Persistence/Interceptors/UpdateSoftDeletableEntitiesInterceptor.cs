using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTemplate.Persistence.Interceptors;

public class UpdateSoftDeletableEntitiesInterceptor(
    IDateTimeProvider dateTimeProvider,
    IServiceScopeFactory serviceScopeFactory)
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
            UpdateSoftDeletableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateSoftDeletableEntities(Microsoft.EntityFrameworkCore.DbContext dbContext)
    {
        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var identityService = serviceProvider.GetService<IIdentityService>();
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted)
                continue;

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedDateUtc)).CurrentValue = dateTimeProvider.UtcNow;
            if (identityService?.UserId is not null)
                entityEntry.Property(nameof(ISoftDeletableEntity.DeletedBy)).CurrentValue = identityService.UserId;

            entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
            return;

        foreach (var referenceEntry in entityEntry.References.Where(r => r.TargetEntry?.State == EntityState.Deleted))
        {
            if (referenceEntry.TargetEntry == null)
                continue;

            referenceEntry.TargetEntry.State = EntityState.Unchanged;
            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }
}