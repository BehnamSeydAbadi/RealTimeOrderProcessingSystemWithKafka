using InventoryService.Domain.DomainService;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Product;
using InventoryService.Infrastructure.DomainService;
using InventoryService.Infrastructure.Inventory;
using InventoryService.Infrastructure.InventoryProduct;
using InventoryService.Infrastructure.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure;

public class InventoryServiceInfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDuplicateInventoryCodeDomainService, DuplicateInventoryCodeDomainService>();

        serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IInventoryProductRepository, InventoryProductRepository>();

        serviceCollection.AddDbContext<InventoryServiceDbContext>(
            options => options.UseInMemoryDatabase($"InventoryServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}