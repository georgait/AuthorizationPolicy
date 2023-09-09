using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PaymentGateway.Infrastructure.Data;
using PaymentGateway.Infrastructure.Data.Base;
using PaymentGateway.Infrastructure.Data.Entities;
using PaymentGateway.Infrastructure.Data.EventHandlers;
using PaymentGateway.Infrastructure.Data.Events;
using PaymentGateway.Infrastructure.Messaging;
using PaymentGateway.Infrastructure.Messaging.Interfaces;
using System.Reflection;

namespace PaymentGateway.HostingExtensions;

public static class ServiceCollectionExt
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(Program).Assembly);            
        });

        services.TryAddTransient(typeof(PaymentConfirmedHandler));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IRepository<Payment>, PaymentRepository>();

        services.AddScoped<IEventDispatcher, EventDispatcher>();

        services.AddScoped<IMessagePublisher<PaymentConfirmedEvent>, RabbitMQPaymentConfirmationPublisher>();
    }
}
