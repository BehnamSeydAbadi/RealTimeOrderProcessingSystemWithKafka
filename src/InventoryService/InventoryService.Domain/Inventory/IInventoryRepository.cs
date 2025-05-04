using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory;

public interface IInventoryRepository
{
    Task<T[]> GetAsync<T>(CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification);
    Task<bool> AnyAsync(CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification);
}