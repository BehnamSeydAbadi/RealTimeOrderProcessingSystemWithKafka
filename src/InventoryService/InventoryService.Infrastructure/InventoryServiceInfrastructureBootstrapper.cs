using InventoryService.Domain.DomainService;
using InventoryService.Domain.Inventory;
using InventoryService.Infrastructure.DomainService;
using InventoryService.Infrastructure.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure;

public class InventoryServiceInfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDuplicateInventoryCodeDomainService, DuplicateInventoryCodeDomainService>();

        serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();

        serviceCollection.AddDbContext<InventoryServiceDbContext>(
            options => options.UseInMemoryDatabase($"InventoryServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}