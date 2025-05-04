using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Specifications;

public class InventoryGetByIdSpecification : AbstractSpecification
{
    public Guid InventoryId { get; }

    public InventoryGetByIdSpecification(Guid inventoryId)
    {
        InventoryId = inventoryId;
    }
}