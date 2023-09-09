namespace PaymentGateway.Infrastructure.Data.Base;

public interface IEventDispatcher
{
    Task DispatchAsync<TId>(IEnumerable<Entity<TId>> entities);
}
