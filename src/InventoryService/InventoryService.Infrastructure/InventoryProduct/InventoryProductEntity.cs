using InventoryService.Infrastructure.Common;
using InventoryService.Infrastructure.Inventory;
using InventoryService.Infrastructure.Product;

namespace InventoryService.Infrastructure.InventoryProduct;

public class InventoryProductEntity : AbstractPersistenceEntity
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Guid ProductId { get; set; }

    public InventoryEntity Inventory { get; set; }
    public ProductEntity Product { get; set; }
}