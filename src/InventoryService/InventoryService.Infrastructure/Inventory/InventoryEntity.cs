using InventoryService.Infrastructure.Common;
using InventoryService.Infrastructure.InventoryProduct;

namespace InventoryService.Infrastructure.Inventory;

public class InventoryEntity : AbstractPersistenceEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisteredAt { get; set; }

    public List<InventoryProductEntity> InventoryProducts { get; set; }
}