using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Data.Base;
using PaymentGateway.Infrastructure.Data.Configuration;
using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly IEventDispatcher _dispatcher;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var entitiesWithEvents = ChangeTracker
             .Entries()
             .Select(e => e.Entity as Entity<Guid>)
             .Where(e => e?.Events is not null && e.Events.Any())
             .ToArray();

        if (entitiesWithEvents.Length > 0)
            await _dispatcher.DispatchAsync(entitiesWithEvents!);

        return result;
    }
}
