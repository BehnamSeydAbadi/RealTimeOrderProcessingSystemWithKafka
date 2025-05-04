using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Specifications;

public class InventoryByCodeSpecification : AbstractSpecification
{
    public InventoryByCodeSpecification(string code)
    {
        Code = code;
    }

    public string Code { get; }
}