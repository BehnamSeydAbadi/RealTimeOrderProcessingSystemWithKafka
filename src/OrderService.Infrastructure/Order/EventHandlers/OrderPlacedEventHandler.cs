using Mapster;
using Mediator;
using OrderService.Domain.Order.Events;

namespace OrderService.Infrastructure.Order.EventHandlers;

public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly OrderServiceDbContext _dbContext;

    public OrderPlacedEventHandler(OrderServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        _dbContext.Set<OrderEntity>().Add(notification.Adapt<OrderEntity>());
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}