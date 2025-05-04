using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Specifications;
using Mediator;

namespace InventoryService.Application.Query.Inventory;

public class GetInventoryProductsQueryHandler : IQueryHandler<GetInventoryProductsQuery, ProductViewModel[]>
{
    private readonly IInventoryProductRepository _repository;

    public GetInventoryProductsQueryHandler(IInventoryProductRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<ProductViewModel[]> Handle(
        GetInventoryProductsQuery query, CancellationToken cancellationToken
    )
    {
        return await _repository.GetAsync<ProductViewModel>(
            cancellationToken,
            new InventoryProductIncludeProductSpecification(),
            new InventoryProductGetByInventoryIdSpecification(query.Id)
        );
    }
}