using Microsoft.EntityFrameworkCore.Storage;

namespace DDDTemplate.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    void ClearChangeTracker();
}