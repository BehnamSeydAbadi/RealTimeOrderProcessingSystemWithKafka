using InventoryService.Domain.Common;

namespace InventoryService.Domain.Product;

public class ProductModel(
    Guid id,
    string name,
    string stockKeepingUnit,
    string color,
    ProductUnitOfMeasure unitOfMeasure,
    bool isPerishable,
    string dimensions,
    string weight,
    ProductCategory category
) : AbstractEntity
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string StockKeepingUnit { get; private set; } = stockKeepingUnit;
    public string Color { get; private set; } = color;
    public ProductUnitOfMeasure UnitOfMeasure { get; private set; } = unitOfMeasure;
    public bool IsPerishable { get; private set; } = isPerishable;
    public string Dimensions { get; private set; } = dimensions;
    public string Weight { get; private set; } = weight;
    public ProductCategory Category { get; private set; } = category;
}