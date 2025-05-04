using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory;

public interface IInventoryRepository : IAbstractRepository
{
    Task<T[]> GetProductsAsync<T>(CancellationToken cancellationToken, Guid inventoryId);
}