using PaymentGateway.Infrastructure.Data.Base;

namespace PaymentGateway.Infrastructure.Messaging.Interfaces;

public interface IMessagePublisher<in TEvent> : IDisposable where TEvent : Event
{
    void Publish(TEvent @event);
}
