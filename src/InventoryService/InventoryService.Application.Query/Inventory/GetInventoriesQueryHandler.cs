using InventoryService.Application.Query.Inventory.ViewModels;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Specifications;
using Mediator;

namespace InventoryService.Application.Query.Inventory;

public class GetInventoriesQueryHandler : IQueryHandler<GetInventoriesQuery, InventoryViewModel[]>
{
    private readonly IInventoryRepository _repository;

    public GetInventoriesQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<InventoryViewModel[]> Handle(GetInventoriesQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync<InventoryViewModel>(
            cancellationToken, new InventoryGetAllSpecification()
        );
    }
}