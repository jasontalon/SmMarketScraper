using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmMarketScraper.Application.Common.Interfaces;
using SmMarketScraper.Domain.Entities;

namespace SmMarketScraper.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        modelBuilder.Entity<ProductHistory>().HasKey(p =>
            new
            {
                p.ProductId, p.Property, p.AggregateId
            });
        modelBuilder.Entity<ProductHistory>().Property(p => p.Property).HasMaxLength(32);
    }

    public static DataContext Create(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(connectionString);

        return new DataContext(optionsBuilder.Options);
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductHistory> ProductHistories { get; set; }
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var connectionString = args.FirstOrDefault();
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException("Connection string is missing");

        return DataContext.Create(connectionString);
    }
}