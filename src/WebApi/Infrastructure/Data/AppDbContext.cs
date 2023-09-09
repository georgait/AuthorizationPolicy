using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Data.Entities;

namespace WebApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions { get; set; } = null!;
}
