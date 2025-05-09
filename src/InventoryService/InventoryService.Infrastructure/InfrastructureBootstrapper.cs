using Confluent.Kafka;
using InventoryService.Domain.DomainService;
using InventoryService.Domain.Inventory;
using InventoryService.Domain.Product;
using InventoryService.Infrastructure.DomainService;
using InventoryService.Infrastructure.Inventory;
using InventoryService.Infrastructure.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        serviceCollection.AddScoped<IDuplicateInventoryCodeDomainService, DuplicateInventoryCodeDomainService>();

        serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();

        serviceCollection.AddSingleton<IConsumer<string, string>>(_ =>
        {
            var keyValuePairs = new ConsumerConfig
            {
                BootstrapServers = configurationManager["Kafka:BootstrapServers"],
                GroupId = "order-processing-group"
            };
            
            var consumerBuilder = new ConsumerBuilder<string, string>(keyValuePairs);
            
            var consumer = consumerBuilder.Build();
            
            return consumer;
        });

        serviceCollection.AddHostedService<InboxBackgroundService>();

        serviceCollection.AddDbContext<OutboxInboxDbContext>(
            options => options.UseInMemoryDatabase($"OutboxInboxDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );

        serviceCollection.AddDbContext<InventoryServiceDbContext>(
            options => options.UseInMemoryDatabase($"InventoryServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}