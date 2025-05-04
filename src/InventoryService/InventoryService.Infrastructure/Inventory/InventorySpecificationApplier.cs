using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Specifications;

namespace InventoryService.Infrastructure.Inventory;

public static class InventorySpecificationApplier
{
    public static IQueryable<InventoryEntity> ApplySpecifications(
        this IQueryable<InventoryEntity> query, params AbstractSpecification[] specifications
    )
    {
        foreach (var specification in specifications)
        {
            query = specification switch
            {
                InventoryByCodeSpecification inventoryByCodeSpecification
                    => query.Where(i => i.Code == inventoryByCodeSpecification.Code),

                _ => throw new ArgumentOutOfRangeException(nameof(specification))
            };
        }

        return query;
    }
}