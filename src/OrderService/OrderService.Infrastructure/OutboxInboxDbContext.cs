using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.OutboxMessages;

namespace OrderService.Infrastructure;

public class OutboxInboxDbContext(DbContextOptions<OutboxInboxDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxMessageConfiguration).Assembly);
    }
}