using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order;
using OrderService.Infrastructure.DomainServices;
using OrderService.Infrastructure.Order;

namespace OrderService.Infrastructure;

public class OrderServiceInfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerDomainService, CustomerDomainService>();
        serviceCollection.AddScoped<IProductDomainService, ProductDomainService>();

        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();

        serviceCollection.AddDbContext<OrderServiceDbContext>(
            options => options.UseInMemoryDatabase($"OrderServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}