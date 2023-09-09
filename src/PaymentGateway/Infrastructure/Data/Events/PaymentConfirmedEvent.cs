using PaymentGateway.Infrastructure.Data.Base;
using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.Infrastructure.Data.Events;

public class PaymentConfirmedEvent : Event
{
    public PaymentConfirmedEvent(Guid subscriptionId, PaymentStatus status, DateTime updatedAt)
    {
        SubscriptionId = subscriptionId;
        Status = status;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid SubscriptionId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
