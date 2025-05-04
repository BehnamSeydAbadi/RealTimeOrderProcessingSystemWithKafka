using InventoryService.Domain.Product;
using Mediator;

namespace InventoryService.Application.Command.Inventory;

public class AddProductToInventoryCommand : ICommand<Guid>
{
    public Guid InventoryId { get; set; }
    public string Name { get; set; }
    public string StockKeepingUnit { get; set; }
    public string Color { get; set; }
    public ProductUnitOfMeasure UnitOfMeasure { get; set; }
    public bool IsPerishable { get; set; }
    public string Dimensions { get; set; }
    public string Weight { get; set; }
    public ProductCategory Category { get; set; }
}