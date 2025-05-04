using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure.Inventory;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryServiceDbContext _dbContext;

    public InventoryRepository(InventoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T[]> GetAsync<T>(params AbstractSpecification[] abstractSpecification)
    {
        return await _dbContext.Set<InventoryEntity>().ApplySpecifications(abstractSpecification)
            .ProjectToType<T>().ToArrayAsync();
    }

    public async Task<bool> AnyAsync(params AbstractSpecification[] abstractSpecification)
    {
        return await _dbContext.Set<InventoryEntity>().ApplySpecifications(abstractSpecification).AnyAsync();
    }
}