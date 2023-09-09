namespace PaymentGateway.Infrastructure.Data.Base;

public abstract class Entity<TId>
{
    public TId Id { get; set; } = default!;

    private readonly List<Event> _events = new();

    public IEnumerable<Event> Events => _events.AsReadOnly();

    protected void RegisterEvent(Event @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}
