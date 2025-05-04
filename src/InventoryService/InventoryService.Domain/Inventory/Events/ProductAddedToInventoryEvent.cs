using InventoryService.Domain.Common;
using InventoryService.Domain.Product;

namespace InventoryService.Domain.Inventory.Events;

public record ProductAddedToInventoryEvent(
    Guid AggregateRootId,
    Guid ProductId,
    string ProductName,
    string ProductStockKeepingUnit,
    string ProductColor,
    ProductUnitOfMeasure ProductUnitOfMeasure,
    bool ProductIsPerishable,
    string ProductDimensions,
    string ProductWeight,
    ProductCategory ProductCategory
) : AbstractDomainEvent;