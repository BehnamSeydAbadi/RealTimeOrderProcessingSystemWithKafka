using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Domain.Product;
using Mediator;

namespace InventoryService.Application.Query.Inventory;

public class GetInventoryProductsQuery(Guid id) : IQuery<ProductViewModel[]>
{
    public Guid Id { get; } = id;
}