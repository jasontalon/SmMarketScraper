using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SmMarketScraper.Application.Common.Interfaces;
using SmMarketScraper.Infrastructure.Persistence;

namespace SmMarketScraper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<ISmMarketClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://consumer.shopsm.com/api/v1");
                client.Timeout = TimeSpan.FromMinutes(1);
            });

        serviceCollection.AddScoped<ISmMarketService>(provider => provider.GetService<ISmMarketClient>()!);
        serviceCollection.AddScoped<IProductInterceptor, ProductInterceptor>();

        serviceCollection.AddDbContext<IDataContext, DataContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(serviceProvider.GetService<IProductInterceptor>()!);
        });

        return serviceCollection;
    }
}