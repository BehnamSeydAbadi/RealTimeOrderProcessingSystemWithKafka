namespace InventoryService.Domain.Common;

public abstract class AbstractAggregateRoot
{
    private readonly Queue<AbstractDomainEvent> _domainEvents = [];

    protected void EnqueueDomainEvent(AbstractDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);

    public Queue<AbstractDomainEvent> GetDomainEventsQueue() => _domainEvents;

    protected void Mutate(params AbstractDomainEvent[] domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            When(domainEvent);
        }
    }

    protected abstract void When(AbstractDomainEvent domainEvent);
}