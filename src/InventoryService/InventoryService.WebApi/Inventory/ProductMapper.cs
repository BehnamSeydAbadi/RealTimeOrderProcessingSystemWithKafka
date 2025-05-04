using InventoryService.Domain.Inventory.Events;
using InventoryService.Domain.Product;
using Mapster;

namespace InventoryService.WebApi.Inventory;

public class ProductMapper
{
    public static void Register()
    {
        TypeAdapterConfig<ProductAddedToInventoryEvent, ProductModel>.NewConfig();
    }
}