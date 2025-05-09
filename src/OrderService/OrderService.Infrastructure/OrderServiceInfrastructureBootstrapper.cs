using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order;
using OrderService.Infrastructure.DomainServices;
using OrderService.Infrastructure.Order;
using OrderService.Infrastructure.Publishers;
using OrderService.Infrastructure.Publishers.Kafka;

namespace OrderService.Infrastructure;

public class OrderServiceInfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        serviceCollection.AddScoped<ICustomerDomainService, CustomerDomainService>();
        serviceCollection.AddScoped<IProductDomainService, ProductDomainService>();

        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();

        serviceCollection.AddSingleton<IProducer, KafkaProducer>();

        serviceCollection.AddSingleton<IProducer<Null, string>>(_ => new ProducerBuilder<Null, string>(
            new ProducerConfig { BootstrapServers = configurationManager["Kafka:BootstrapServers"] }
        ).Build());

        serviceCollection.AddHostedService<OutboxBackgroundService>();

        serviceCollection.AddDbContext<OutboxInboxDbContext>(
            options => options.UseInMemoryDatabase($"OutboxInboxDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );

        serviceCollection.AddDbContext<OrderServiceDbContext>(
            options => options.UseInMemoryDatabase($"OrderServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}