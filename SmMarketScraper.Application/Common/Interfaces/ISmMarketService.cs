using SmMarketScraper.Application.Shared.Models;

public interface ISmMarketService
{
    public Task<SmMarketResponse> GetAsync(SmMarketRequestBody body);
}