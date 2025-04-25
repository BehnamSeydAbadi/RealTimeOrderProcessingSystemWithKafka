using Mediator;
using OrderService.Domain.Order.Events;

namespace OrderService.Infrastructure.Order.EventHandlers;

public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
{
    public async ValueTask Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}