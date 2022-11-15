namespace SmMarketScraper.Domain.Entities;

public class Product
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Url { get; set; }
    public string? UoM { get; set; }
    public decimal? FinalPrice { get; set; }
    public decimal? RegularPrice { get; set; }
    public decimal? Discount { get; set; }
    public decimal? MaxQuantity { get; set; }
    public DateTimeOffset? CreatedAtUtc { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }

    public virtual ICollection<ProductHistory>? ProductHistories { get; set; }
}