namespace InventoryService.Domain.Common;

public interface IAbstractRepository
{
    Task<T[]> GetAsync<T>(CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification);
    Task<bool> AnyAsync(CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification);
}