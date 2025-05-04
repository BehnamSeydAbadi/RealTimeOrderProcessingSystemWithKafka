using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory;

public interface IInventoryRepository
{
    Task<T[]> GetAsync<T>(params AbstractSpecification[] abstractSpecification);
    Task<bool> AnyAsync(params AbstractSpecification[] abstractSpecification);
}