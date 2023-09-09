using MediatR;
using PaymentGateway.Infrastructure.Data.Events;
using PaymentGateway.Infrastructure.Messaging.Interfaces;

namespace PaymentGateway.Infrastructure.Data.EventHandlers;

public class PaymentConfirmedHandler : INotificationHandler<PaymentConfirmedEvent>
{
    private readonly IMessagePublisher<PaymentConfirmedEvent> _publisher;

    public PaymentConfirmedHandler(IMessagePublisher<PaymentConfirmedEvent> publisher)
    {
        _publisher = publisher;
    }

    public Task Handle(PaymentConfirmedEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Publish(notification);

        return Task.CompletedTask;
    }
}
