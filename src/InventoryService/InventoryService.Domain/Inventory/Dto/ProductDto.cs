using InventoryService.Domain.Product;

namespace InventoryService.Domain.Inventory.Dto;

public record ProductDto
{
    public string Name { get; set; }
    public string StockKeepingUnit { get; set; }
    public string Color { get; set; }
    public ProductUnitOfMeasure UnitOfMeasure { get; set; }
    public bool IsPerishable { get; set; }
    public string Dimensions { get; set; }
    public string Weight { get; set; }
    public ProductCategory Category { get; set; }
}