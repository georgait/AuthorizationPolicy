using MediatR;

namespace PaymentGateway.Infrastructure.Data.Base;

public abstract class Event : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
