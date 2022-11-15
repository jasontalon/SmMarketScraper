using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SmMarketScraper.Application.Common.Interfaces;
using SmMarketScraper.Domain.Entities;

namespace SmMarketScraper.Infrastructure.Persistence;

internal class ProductInterceptor : SaveChangesInterceptor, IProductInterceptor
{
    private readonly ILogger<IProductInterceptor> _logger;

    public ProductInterceptor(ILogger<IProductInterceptor> logger)
    {
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var context = eventData.Context!;
            var changeTracker = context.ChangeTracker;

            changeTracker.DetectChanges();

            foreach (var entry in changeTracker.Entries())
            {
                if (entry.State is EntityState.Modified && entry.Entity.GetType().Name.Equals(nameof(Product)))
                    TrackChanges(context, entry);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            throw;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void TrackChanges(DbContext dbContext, EntityEntry entry)
    {
        var context = (DataContext) dbContext;
        var avoidColumns = new[] {nameof(Product.CreatedAtUtc), nameof(Product.UpdatedAtUtc), nameof(Product.Id)};
        var product = entry.Entity as Product;

        var properties = entry.Properties
            .Where(p => !avoidColumns.Contains(p.Metadata.Name))
            .ToList();

        var histories = new List<ProductHistory>();
        var aggregateId = Guid.NewGuid();

        foreach (var property in properties)
        {
            if (property.CurrentValue is null && property.OriginalValue is null) continue;

            var currentValue = property.CurrentValue ?? string.Empty;
            var originalValue = property.OriginalValue ?? string.Empty;

            var isDifferent = !originalValue.Equals(currentValue);
            var isModified = isDifferent && property.IsModified;

            if (!isModified) continue;

            var history = new ProductHistory
            {
                ProductId = product!.Id,
                Property = property.Metadata.Name,
                AggregateId = aggregateId,
                NewValue = currentValue.ToString(),
                OldValue = originalValue.ToString(),
                UpdatedAtUtc = product.UpdatedAtUtc ?? DateTimeOffset.UtcNow
            };
            histories.Add(history);
        }

        context.ProductHistories.AddRange(histories);
        context.SaveChanges();
    }
}