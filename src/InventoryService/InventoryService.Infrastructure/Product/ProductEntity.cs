using System.Drawing;
using InventoryService.Domain.Product;
using InventoryService.Infrastructure.Common;
using InventoryService.Infrastructure.InventoryProduct;

namespace InventoryService.Infrastructure.Product;

public class ProductEntity : AbstractPersistenceEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string StockKeepingUnit { get; set; }
    public string Color { get; set; }
    public ProductUnitOfMeasure UnitOfMeasure { get; set; }
    public bool IsPerishable { get; set; }
    public string Dimensions { get; set; }
    public string Weight { get; set; }
    public ProductCategory Category { get; set; }

    public List<InventoryProductEntity> InventoryProducts { get; set; }
}