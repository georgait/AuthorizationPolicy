using PaymentGateway.Infrastructure.Data.Base;
using PaymentGateway.Infrastructure.Data.Events;

namespace PaymentGateway.Infrastructure.Data.Entities;

public class Payment : Entity<Guid>
{
    public Guid SubscriptiontId { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Payment(Guid id, Guid subscriptiontId)
    {
        Id = id;
        SubscriptiontId = subscriptiontId;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void ConfirmPayment()
    {
        Status = PaymentStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;
        RegisterEvent(new PaymentConfirmedEvent(SubscriptiontId, Status, DateTime.UtcNow));
    }
}
