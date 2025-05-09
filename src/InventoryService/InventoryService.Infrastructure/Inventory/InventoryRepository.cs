using InventoryService.Domain.Inventory;
using InventoryService.Domain.Product;
using InventoryService.Infrastructure.Common;
using InventoryService.Infrastructure.InventoryProduct;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Inventory;

public class InventoryRepository : AbstractRepository<InventoryEntity>, IInventoryRepository
{
    public InventoryRepository(InventoryServiceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<T[]> GetProductsAsync<T>(Guid inventoryId, CancellationToken cancellationToken)
    {
        return await DbContext.Set<InventoryProductEntity>()
            .Include(ip => ip.Product)
            .Where(ip => ip.InventoryId == inventoryId)
            .Select(ip => ip.Product)
            .ProjectToType<T>()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<T[]> GetInventoriesByProductIdsAsync<T>(Guid[] productIds, CancellationToken cancellationToken)
    {
        return await DbContext.Set<InventoryProductEntity>()
            .Include(ip => ip.Inventory)
            .Where(ip => productIds.Contains(ip.ProductId))
            .Select(ip => ip.Inventory)
            .ProjectToType<T>()
            .ToArrayAsync(cancellationToken);
    }
}