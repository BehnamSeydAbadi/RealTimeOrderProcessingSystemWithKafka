using InventoryService.Domain.Inventory.Events;
using Mapster;
using Mediator;

namespace InventoryService.Infrastructure.Inventory.EventHandlers;

public class InventoryRegisteredEventHandler : INotificationHandler<InventoryRegisteredEvent>
{
    private readonly InventoryServiceDbContext _dbContext;

    public InventoryRegisteredEventHandler(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask Handle(InventoryRegisteredEvent notification, CancellationToken cancellationToken)
    {
        _dbContext.Set<InventoryEntity>().Add(notification.Adapt<InventoryEntity>());
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}