using InventoryService.Domain.Inventory;
using InventoryService.Infrastructure.Common;

namespace InventoryService.Infrastructure.Inventory;

public class InventoryRepository : AbstractRepository<InventoryEntity>, IInventoryRepository
{
    public InventoryRepository(InventoryServiceDbContext dbContext) : base(dbContext)
    {
    }
}