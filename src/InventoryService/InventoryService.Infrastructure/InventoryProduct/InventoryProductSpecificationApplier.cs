using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Specifications;
using InventoryService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.InventoryProduct;

public static class InventoryProductSpecificationApplier
{
    public static IQueryable<InventoryProductEntity> ApplySpecifications(
        this IQueryable<AbstractPersistenceEntity> query, params AbstractSpecification[] specifications
    )
    {
        var inventoryProductQuery = (query as IQueryable<InventoryProductEntity>)!;

        foreach (var specification in specifications)
        {
            inventoryProductQuery = specification switch
            {
                InventoryProductIncludeProductSpecification
                    => inventoryProductQuery.Include(ip => ip.Product),

                InventoryProductGetByInventoryIdSpecification getByInventoryIdSpec
                    => inventoryProductQuery.Where(ip => ip.InventoryId == getByInventoryIdSpec.InventoryId),

                _ => throw new ArgumentOutOfRangeException(nameof(specification))
            };
        }

        return inventoryProductQuery;
    }
}