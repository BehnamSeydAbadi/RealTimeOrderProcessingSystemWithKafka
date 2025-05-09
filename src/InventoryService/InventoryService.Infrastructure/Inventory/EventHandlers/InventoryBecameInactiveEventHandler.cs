using InventoryService.Domain.Inventory.Events;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Inventory.EventHandlers;

public class InventoryBecameInactiveEventHandler(InventoryServiceDbContext dbContext)
    : INotificationHandler<InventoryBecameInactiveEvent>
{
    public async ValueTask Handle(InventoryBecameInactiveEvent notification, CancellationToken cancellationToken)
    {
        var inventoryEntity = await dbContext.Set<InventoryEntity>()
            .SingleAsync(i => i.Id == notification.Id, cancellationToken);

        inventoryEntity.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}