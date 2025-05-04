using InventoryService.Domain.Inventory.Events;
using Mediator;

namespace InventoryService.Infrastructure.InventoryProduct.EventHandler;

public class ProductAddedToInventoryEventHandler : INotificationHandler<ProductAddedToInventoryEvent>
{
    private readonly InventoryServiceDbContext _dbContext;

    public ProductAddedToInventoryEventHandler(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask Handle(ProductAddedToInventoryEvent notification, CancellationToken cancellationToken)
    {
        _dbContext.Set<InventoryProductEntity>().Add(new InventoryProductEntity
        {
            InventoryId = notification.AggregateRootId,
            ProductId = notification.ProductId,
        });
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}