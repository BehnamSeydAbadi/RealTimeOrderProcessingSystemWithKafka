using InventoryService.Domain.Common;
using Mediator;

namespace InventoryService.Application.Command.Common;

public abstract class AbstractRequestHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IPublisher Publisher;

    public AbstractRequestHandler(IPublisher publisher)
    {
        Publisher = publisher;
    }

    public abstract ValueTask<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task PublishDomainEventsAsync(Queue<AbstractDomainEvent> domainEventsQueue)
    {
        while (domainEventsQueue.Any())
        {
            await Publisher.Publish(domainEventsQueue.Dequeue());
        }
    }
}