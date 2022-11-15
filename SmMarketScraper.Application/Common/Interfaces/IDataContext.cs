using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SmMarketScraper.Domain.Entities;

namespace SmMarketScraper.Application.Common.Interfaces;

public interface IDataContext
{
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry Entry(object entity);
    DbSet<Product> Products { get; set; }
}