using InventoryService.Domain.Inventory.Events;
using Mapster;
using Mediator;

namespace InventoryService.Infrastructure.Product.EventHandlers;

public class ProductAddedToInventoryEventHandler : INotificationHandler<ProductAddedToInventoryEvent>
{
    private readonly InventoryServiceDbContext _dbContext;

    public ProductAddedToInventoryEventHandler(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask Handle(ProductAddedToInventoryEvent notification, CancellationToken cancellationToken)
    {
        _dbContext.Set<ProductEntity>().Add(notification.Adapt<ProductEntity>());
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}