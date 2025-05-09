using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Domain.Inventory;
using Mediator;

namespace InventoryService.Application.Query.Inventory;

public class GetInventoryProductsQueryHandler : IQueryHandler<GetInventoryProductsQuery, ProductViewModel[]>
{
    private readonly IInventoryRepository _repository;

    public GetInventoryProductsQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<ProductViewModel[]> Handle(
        GetInventoryProductsQuery query, CancellationToken cancellationToken
    )
    {
        return await _repository.GetProductsAsync<ProductViewModel>(inventoryId: query.Id, cancellationToken: cancellationToken);
    }
}