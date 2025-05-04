using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Specifications;

public class InventoryProductGetByInventoryIdSpecification(Guid inventoryId) : AbstractSpecification
{
    public Guid InventoryId { get; } = inventoryId;
}