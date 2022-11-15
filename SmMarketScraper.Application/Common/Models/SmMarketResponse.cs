namespace SmMarketScraper.Application.Shared.Models;

public sealed class SmMarketResponse
{
    public bool? Success { get; set; }

    public SmMarketResponseData? Data { get; set; }
}

public sealed class SmMarketResponseData
{
    public List<SmMarketItem>? Items { get; set; }

    public long? TotalCount { get; set; }

    public long? PageSize { get; set; }
}

public sealed class SmMarketItem
{
    public string? Url { get; set; }

    public string? Id { get; set; }

    public string? DescriptionHtml { get; set; }

    public Uri? Image { get; set; }

    public string? Name { get; set; }

    public decimal? FinalPrice { get; set; }

    public decimal? RegularPrice { get; set; }

    public decimal? Discount { get; set; }

    public decimal? MaxQuantity { get; set; }

    public string? Uom { get; set; }
}

public class SmMarketRequestBody
{
    public SmMarketRequestBody(int pageSize, int currentPage)
    {
        PageSize = pageSize;
        CurrentPage = currentPage;
    }

    public int PageSize { get; }
    public int CurrentPage { get; }
}