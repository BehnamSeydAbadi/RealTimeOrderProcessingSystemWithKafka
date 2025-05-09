using InventoryService.Domain.Inventory.Events;
using Mapster;
using Mediator;

namespace InventoryService.Infrastructure.Inventory.EventHandlers;

public class InventoryRegisteredEventHandler(InventoryServiceDbContext dbContext)
    : INotificationHandler<InventoryRegisteredEvent>
{
    public async ValueTask Handle(InventoryRegisteredEvent notification, CancellationToken cancellationToken)
    {
        dbContext.Set<InventoryEntity>().Add(notification.Adapt<InventoryEntity>());
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}