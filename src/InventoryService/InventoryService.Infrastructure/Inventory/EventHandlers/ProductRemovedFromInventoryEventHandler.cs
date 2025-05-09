using InventoryService.Domain.Inventory.Events;
using InventoryService.Infrastructure.InventoryProduct;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Inventory.EventHandlers;

public class ProductRemovedFromInventoryEventHandler(InventoryServiceDbContext dbContext)
    : INotificationHandler<ProductRemovedFromInventoryEvent>
{
    public async ValueTask Handle(ProductRemovedFromInventoryEvent notification, CancellationToken cancellationToken)
    {
        var inventoryProductEntity = await dbContext.Set<InventoryProductEntity>()
            .SingleAsync(ip => ip.ProductId == notification.ProductId, cancellationToken);

        dbContext.Set<InventoryProductEntity>().Remove(inventoryProductEntity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}