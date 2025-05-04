using InventoryService.Domain.Common;
using Mediator;

namespace InventoryService.Application.Command.Common;

public abstract class AbstractCommandHandler<TCommand, TResponse>
    : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    protected readonly IPublisher Publisher;

    public AbstractCommandHandler(IPublisher publisher)
    {
        Publisher = publisher;
    }

    public abstract ValueTask<TResponse> Handle(TCommand request, CancellationToken cancellationToken);

    protected async Task PublishDomainEventsAsync(Queue<AbstractDomainEvent> domainEventsQueue)
    {
        while (domainEventsQueue.Any())
        {
            await Publisher.Publish(domainEventsQueue.Dequeue());
        }
    }
}