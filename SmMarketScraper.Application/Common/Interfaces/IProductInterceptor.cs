using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SmMarketScraper.Application.Common.Interfaces;

public interface IProductInterceptor : IInterceptor
{
    ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken());

    void TrackChanges(DbContext dbContext, EntityEntry entry);
    InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result);
    int SavedChanges(SaveChangesCompletedEventData eventData, int result);
    void SaveChangesFailed(DbContextErrorEventData eventData);

    ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken);

    Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken);
    void SaveChangesCanceled(DbContextEventData eventData);
    Task SaveChangesCanceledAsync(DbContextEventData eventData, CancellationToken cancellationToken);
    InterceptionResult ThrowingConcurrencyException(ConcurrencyExceptionEventData eventData, InterceptionResult result);

    ValueTask<InterceptionResult> ThrowingConcurrencyExceptionAsync(ConcurrencyExceptionEventData eventData,
        InterceptionResult result, CancellationToken cancellationToken);
}