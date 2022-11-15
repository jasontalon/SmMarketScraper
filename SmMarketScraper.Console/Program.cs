using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmMarketScraper.Application;
using SmMarketScraper.Application.Handlers.Commands;
using SmMarketScraper.Application.Common.Interfaces;
using SmMarketScraper.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddEnvironmentVariables())
    .ConfigureServices(ConfigureServices).Build();

void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
{
    collection
        .AddInfrastructure(context.Configuration)
        .AddApplication();
}

var logger = host.Services.GetService<ILogger<Program>>();

await host.Services.GetService<IDataContext>()!.Database.MigrateAsync();

var stopWatch = new Stopwatch();

logger!.LogInformation("Begin");
stopWatch.Start();
var mediator = host.Services.GetService<IMediator>();

await mediator!.Send(new SyncProductsCommand());

stopWatch.Stop();
logger!.LogInformation($"Finished. Elapsed time {stopWatch.Elapsed.ToString(@"m\:ss")}");