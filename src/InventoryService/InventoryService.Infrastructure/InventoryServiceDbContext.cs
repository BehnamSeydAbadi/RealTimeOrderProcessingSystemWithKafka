using InventoryService.Infrastructure.Inventory;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure;

public class InventoryServiceDbContext : DbContext
{
    public InventoryServiceDbContext(DbContextOptions<InventoryServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryConfiguration).Assembly);
    }
}