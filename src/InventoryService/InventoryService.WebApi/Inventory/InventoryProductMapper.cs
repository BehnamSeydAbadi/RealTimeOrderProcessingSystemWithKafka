using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Infrastructure.InventoryProduct;
using Mapster;

namespace InventoryService.WebApi.Inventory;

public class InventoryProductMapper
{
    public static void Register()
    {
        TypeAdapterConfig<InventoryProductEntity, ProductViewModel>.NewConfig()
            .Map(dest => dest.Id, src => src.ProductId)
            .Map(dest => dest.InventoryId, src => src.InventoryId)
            .Map(dest => dest.Name, src => src.Product.Name)
            .Map(dest => dest.StockKeepingUnit, src => src.Product.StockKeepingUnit)
            .Map(dest => dest.Color, src => src.Product.Color)
            .Map(dest => dest.UnitOfMeasure, src => src.Product.UnitOfMeasure)
            .Map(dest => dest.IsPerishable, src => src.Product.IsPerishable)
            .Map(dest => dest.Dimensions, src => src.Product.Dimensions)
            .Map(dest => dest.Weight, src => src.Product.Weight)
            .Map(dest => dest.Category, src => src.Product.Category);
    }
}