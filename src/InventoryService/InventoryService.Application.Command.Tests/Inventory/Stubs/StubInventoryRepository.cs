using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory;
using InventoryService.Infrastructure.Inventory;
using Mapster;

namespace InventoryService.Application.Command.Tests.Inventory.Stubs;

public class StubInventoryRepository : IInventoryRepository
{
    private readonly List<InventoryEntity> _inventories = new();

    private StubInventoryRepository()
    {
    }

    public static StubInventoryRepository New() => new();


    public StubInventoryRepository WithInventory(InventoryEntity inventory)
    {
        _inventories.Add(inventory);
        return this;
    }

    public async Task<T[]> GetAsync<T>(
        CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification
    )
    {
        await Task.CompletedTask;
        return _inventories.AsQueryable().ApplySpecifications(abstractSpecification).ProjectToType<T>().ToArray();
    }

    public async Task<bool> AnyAsync(
        CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification
    )
    {
        await Task.CompletedTask;
        return _inventories.AsQueryable().ApplySpecifications(abstractSpecification).Any();
    }

    public Task<T[]> GetProductsAsync<T>(Guid inventoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T[]> GetInventoriesByProductIdsAsync<T>(Guid[] productIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}