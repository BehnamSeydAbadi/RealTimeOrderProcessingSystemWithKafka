using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory;

public interface IInventoryRepository
{
    Task<Inventory?> GetAsync(params AbstractSpecification[] abstractSpecification);
    Task<bool> AnyAsync(params AbstractSpecification[] abstractSpecification);
}