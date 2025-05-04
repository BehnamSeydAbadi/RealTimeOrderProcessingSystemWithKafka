using InventoryService.Domain.Common;
using InventoryService.Infrastructure.Inventory;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Common;

public class AbstractRepository<TEntity>
    : IAbstractRepository where TEntity : AbstractPersistenceEntity
{
    protected readonly InventoryServiceDbContext DbContext;

    public AbstractRepository(InventoryServiceDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T[]> GetAsync<T>(
        CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification
    )
    {
        return await DbContext.Set<TEntity>().ApplySpecifications(abstractSpecification)
            .ProjectToType<T>().ToArrayAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(
        CancellationToken cancellationToken, params AbstractSpecification[] abstractSpecification
    )
    {
        return await DbContext.Set<TEntity>().ApplySpecifications(abstractSpecification)
            .AnyAsync(cancellationToken);
    }
}