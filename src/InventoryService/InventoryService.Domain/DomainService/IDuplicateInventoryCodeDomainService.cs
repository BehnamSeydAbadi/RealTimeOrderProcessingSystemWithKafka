namespace InventoryService.Domain.DomainService;

public interface IDuplicateInventoryCodeDomainService
{
    Task<bool> IsInventoryCodeDuplicateAsync(string code, CancellationToken cancellationToken);
}