using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RiverBooks.Users.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options, IDomainEventDispatcher? dispatcher) : IdentityDbContext(options)
{
    internal DbSet<ApplicationUser> ApplicationUsers { get; set; }
    internal DbSet<UserAddress> UserAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IHaveDomainEvents).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Ignore(nameof(IHaveDomainEvents.DomainEvents));
            }
        }
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        if (dispatcher is null)
            return result;

        var entitiesWithEvent = ChangeTracker.Entries<IHaveDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await dispatcher.DispatchAndClearEventsAsync(entitiesWithEvent, cancellationToken);

        return result;
    }
}
