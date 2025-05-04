using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Specifications;
using InventoryService.Infrastructure.Common;

namespace InventoryService.Infrastructure.Inventory;

public static class InventorySpecificationApplier
{
    public static IQueryable<InventoryEntity> ApplySpecifications(
        this IQueryable<AbstractPersistenceEntity> query, params AbstractSpecification[] specifications
    )
    {
        var inventoryQuery = (query as IQueryable<InventoryEntity>)!;

        foreach (var specification in specifications)
        {
            inventoryQuery = specification switch
            {
                InventoryGetByCodeSpecification getByCodeSpec
                    => inventoryQuery.Where(i => i.Code == getByCodeSpec.Code),
                InventoryGetAllSpecification
                    => inventoryQuery,
                InventoryGetByIdSpecification getByIdSpec
                    => inventoryQuery.Where(i => i.Id == getByIdSpec.InventoryId),
                _ => throw new ArgumentOutOfRangeException(nameof(specification))
            };
        }

        return inventoryQuery;
    }
}