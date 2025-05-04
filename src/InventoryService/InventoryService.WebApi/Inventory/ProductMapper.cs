using InventoryService.Application.Command.Inventory;
using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Domain.Inventory.Events;
using InventoryService.Domain.Product;
using InventoryService.Infrastructure.Product;
using InventoryService.WebApi.Inventory.Dto;
using Mapster;

namespace InventoryService.WebApi.Inventory;

public class ProductMapper
{
    public static void Register()
    {
        TypeAdapterConfig<ProductAddedToInventoryEvent, ProductModel>.NewConfig()
            .Map(dest => dest.Id, src => src.ProductId);

        TypeAdapterConfig<AddProductToInventoryDto, AddProductToInventoryCommand>.NewConfig();

        TypeAdapterConfig<ProductEntity, ProductViewModel>.NewConfig();

        TypeAdapterConfig<ProductAddedToInventoryEvent, ProductEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.ProductId);
    }
}