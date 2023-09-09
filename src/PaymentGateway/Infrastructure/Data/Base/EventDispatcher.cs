using MediatR;

namespace PaymentGateway.Infrastructure.Data.Base;

public class EventDispatcher : IEventDispatcher
{
    private readonly IMediator _mediator;

    public EventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync<TId>(IEnumerable<Entity<TId>> entities)
    {
        foreach (var entity in entities)
        {
            var events = entity.Events.ToArray();

            entity.ClearEvents();

            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
