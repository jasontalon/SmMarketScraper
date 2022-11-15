using Refit;
using SmMarketScraper.Application.Shared.Models;

public interface ISmMarketClient : ISmMarketService
{
    [Post("/getMarketsProductsV2")]
    public Task<SmMarketResponse> GetAsync([Body] SmMarketRequestBody body);
}