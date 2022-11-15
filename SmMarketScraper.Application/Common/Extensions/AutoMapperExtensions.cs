using AutoMapper;

namespace SmMarketScraper.Application.Common.Extensions;

public static class AutoMapperExtensions
{
    public static string? TryMap<TSource, TDestination>(this IMapper mapper,
        TSource source,
        TDestination destination)
    {
        try
        {
            mapper.Map(source, destination);
            return null;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}