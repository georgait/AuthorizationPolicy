using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text.Json;
using WebApi.AuthorizarionPolicies;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Data.Entities;
using WebApi.Models;

namespace WebApi.Infrastructure;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task ProcessAsync(string message)
    {
        var notification = JsonSerializer.Deserialize<PaymentNotification>(message);
        if (notification is null)
        {
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var subscription = new Subscription
        {
            SubscriptionId = Guid.Parse(notification.SubscriptionId),
            UpdatedAt = notification.UpdatedAt,
            IsActive = notification.Status == PaymentStatus.Confirmed
        };

        await context.Set<Subscription>().AddAsync(subscription);
        await context.SaveChangesAsync();
    }
}
