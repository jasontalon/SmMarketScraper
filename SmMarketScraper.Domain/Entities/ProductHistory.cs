namespace SmMarketScraper.Domain.Entities;

public class ProductHistory
{
    public long ProductId { get; set; }
    public Guid AggregateId { get; set; }
    public string Property { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }

    public virtual Product? Product { get; set; }
}