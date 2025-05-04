using InventoryService.Domain.Inventory;
using InventoryService.Infrastructure.Common;

namespace InventoryService.Infrastructure.InventoryProduct;

public class InventoryProductRepository : AbstractRepository<InventoryProductEntity>, IInventoryProductRepository
{
    public InventoryProductRepository(InventoryServiceDbContext dbContext) : base(dbContext)
    {
    }
}