namespace OrderService.Domain.Common;

public abstract class AbstractAggregateRoot
{
    private readonly Queue<AbstractDomainEvent> _domainEvents = [];

    protected void EnqueueDomainEvent(AbstractDomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);

    public Queue<AbstractDomainEvent> GetDomainEventsQueue() => _domainEvents;
}