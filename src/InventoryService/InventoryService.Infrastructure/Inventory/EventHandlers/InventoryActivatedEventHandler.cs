using InventoryService.Domain.Inventory.Events;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Inventory.EventHandlers;

public class InventoryActivatedEventHandler(InventoryServiceDbContext dbContext)
    : INotificationHandler<InventoryActivatedEvent>
{
    public async ValueTask Handle(InventoryActivatedEvent notification, CancellationToken cancellationToken)
    {
        var inventoryEntity = await dbContext.Set<InventoryEntity>()
            .SingleAsync(i => i.Id == notification.Id, cancellationToken);

        inventoryEntity.IsActive = true;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}