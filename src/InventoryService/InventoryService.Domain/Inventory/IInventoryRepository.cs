using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory;

public interface IInventoryRepository : IAbstractRepository
{
    Task<T[]> GetProductsAsync<T>(Guid inventoryId, CancellationToken cancellationToken);
    Task<T[]> GetInventoriesByProductIdsAsync<T>(Guid[] productIds, CancellationToken cancellationToken);
}