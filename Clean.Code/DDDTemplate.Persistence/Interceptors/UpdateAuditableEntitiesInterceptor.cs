using System.Threading;
using System.Threading.Tasks;
using DDDTemplate.Application.Abstractions.Services;
using DDDTemplate.Domain.Abstractions.Data;
using DDDTemplate.SharedKernel.Abstractions.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTemplate.Persistence.Interceptors;

public class UpdateAuditableEntitiesInterceptor(
    IDateTimeProvider dateTimeProvider,
    IServiceScopeFactory serviceScopeFactory) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
            UpdateAuditableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(Microsoft.EntityFrameworkCore.DbContext dbContext)
    {
        foreach (var entityEntry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
        {
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            var identityService = serviceProvider.GetService<IIdentityService>();
            var isAdd = entityEntry.State == EntityState.Added;
            if (isAdd)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedDateUtc)).CurrentValue = dateTimeProvider.UtcNow;
                if (identityService?.UserId is not null)
                    entityEntry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = identityService.UserId;
            }

            if (isAdd || entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedDateUtc)).CurrentValue = dateTimeProvider.UtcNow;
                if (identityService?.UserId is not null)
                    entityEntry.Property(nameof(IAuditableEntity.ModifiedBy)).CurrentValue = identityService.UserId;
            }
        }
    }
}