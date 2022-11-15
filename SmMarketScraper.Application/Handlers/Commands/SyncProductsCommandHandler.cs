using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmMarketScraper.Application.Common.Extensions;
using SmMarketScraper.Application.Common.Interfaces;
using SmMarketScraper.Application.Shared.Models;
using SmMarketScraper.Domain.Entities;

namespace SmMarketScraper.Application.Handlers.Commands;

internal class SyncProductsCommandHandler : IRequestHandler<SyncProductsCommand>
{
    private readonly ILogger<SyncProductsCommandHandler> _logger;
    private readonly ISmMarketService _smMarketService;
    private readonly IDataContext _dataContext;
    private readonly IMapper _mapper;

    public SyncProductsCommandHandler(ILogger<SyncProductsCommandHandler> logger, ISmMarketService smMarketService,
        IDataContext dataContext, IMapper mapper)
    {
        _logger = logger;
        _smMarketService = smMarketService;
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SyncProductsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var items = new List<SmMarketItem>();
            var currentPage = 0;
            int itemCount;
            do
            {
                var responseObject = await _smMarketService.GetAsync(new SmMarketRequestBody(100, currentPage));

                itemCount = responseObject?.Data?.Items?.Count ?? 0;
                currentPage++;

                if (responseObject is not null)
                    items.AddRange(responseObject.Data?.Items?.Where(p => !string.IsNullOrEmpty(p.Name)) ??
                                   new List<SmMarketItem>());
            } while (itemCount > 0);

            var error = string.Empty;

            foreach (var item in items)
            {
                var entity = await FindOrCreate(item);
                error = _mapper.TryMap(item, entity);

                if (!string.IsNullOrEmpty(error))
                    throw new ArgumentException(error);
                
                var state = _dataContext.Entry(entity).State;

                switch (state)
                {
                    case EntityState.Detached:
                        await _dataContext.Products.AddAsync(entity, cancellationToken);
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAtUtc = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                    case EntityState.Added:
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                await _dataContext.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Unit.Value;
    }

    public async Task<Product> FindOrCreate(SmMarketItem item)
    {
        long.TryParse(item.Id, out long id);
        return await _dataContext.Products.FindAsync(id) ?? new Product
        {
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}

public sealed class SyncProductsCommand : IRequest
{
}