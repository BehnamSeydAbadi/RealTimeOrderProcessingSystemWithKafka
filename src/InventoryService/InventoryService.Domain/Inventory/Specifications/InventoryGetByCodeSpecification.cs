using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Specifications;

public class InventoryGetByCodeSpecification : AbstractSpecification
{
    public InventoryGetByCodeSpecification(string code)
    {
        Code = code;
    }

    public string Code { get; }
}