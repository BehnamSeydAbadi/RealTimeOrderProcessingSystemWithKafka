using InventoryService.Domain.Common;

namespace InventoryService.Domain.Product;

public class ProductModel : AbstractEntity
{
    public ProductModel(
        Guid id, string name, string stockKeepingUnit, string color, ProductUnitOfMeasure unitOfMeasure,
        bool isPerishable, string dimensions, string weight, ProductCategory category
    )
    {
        Id = id;
        Name = name;
        StockKeepingUnit = stockKeepingUnit;
        Color = color;
        UnitOfMeasure = unitOfMeasure;
        IsPerishable = isPerishable;
        Dimensions = dimensions;
        Weight = weight;
        Category = category;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string StockKeepingUnit { get; private set; }
    public string Color { get; private set; }
    public ProductUnitOfMeasure UnitOfMeasure { get; private set; }
    public bool IsPerishable { get; private set; }
    public string Dimensions { get; private set; }
    public string Weight { get; private set; }
    public ProductCategory Category { get; private set; }
}