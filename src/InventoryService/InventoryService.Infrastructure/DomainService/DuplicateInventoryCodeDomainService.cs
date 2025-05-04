using InventoryService.Domain.DomainService;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Inventory.Specifications;

namespace InventoryService.Infrastructure.DomainService;

public class DuplicateInventoryCodeDomainService : IDuplicateInventoryCodeDomainService
{
    private readonly IInventoryRepository _repository;

    public DuplicateInventoryCodeDomainService(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsInventoryCodeDuplicateAsync(string code, CancellationToken cancellationToken)
    {
        return await _repository.AnyAsync(cancellationToken, new InventoryGetByCodeSpecification(code));
    }
}