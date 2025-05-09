using InventoryService.Infrastructure.InboxMessages;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Infrastructure;

public class OutboxInboxDbContext(DbContextOptions<OutboxInboxDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InboxMessageConfiguration).Assembly);
    }
}