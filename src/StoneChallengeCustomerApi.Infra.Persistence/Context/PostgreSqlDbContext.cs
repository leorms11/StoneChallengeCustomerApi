using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using StoneChallengeCustomerApi.Domain.Interfaces;
using StoneChallengeCustomerApi.Domain.Models;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Helpers;
using StoneChallengeCustomerApi.Infra.Persistence.Mappings;

namespace StoneChallengeCustomerApi.Infra.Persistence.Context;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
        ChangeTracker.Tracked += OnEntityStateTracked;
        ChangeTracker.StateChanged += OnEntityStateChange;
    }
    public DbSet<Customer> Customers;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CustomerMap());
    }

    private void OnEntityStateTracked(object sender, EntityTrackedEventArgs args)
    {
        if (args.Entry.Entity is ITimeStamped entity && args.Entry.State == EntityState.Added)
        {
            entity.CreatedAt = DateTimeHelpers.GetSouthAmericaDateTimeNow();
            entity.LastModifiedAt = DateTimeHelpers.GetSouthAmericaDateTimeNow();
        }
    }
    
    private void OnEntityStateChange(object sender, EntityStateChangedEventArgs args)
    {
        if (args.Entry.Entity is ITimeStamped entity && args.Entry.State == EntityState.Modified)
        {
            entity.LastModifiedAt = DateTimeHelpers.GetSouthAmericaDateTimeNow();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseLoggerFactory(LoggerFactory.Create(config =>
            {
                config.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                config.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
            }));
    }
}