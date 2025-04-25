using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<OrderServiceDbContext>(
            options => options.UseInMemoryDatabase($"OrderServiceDb_{Guid.NewGuid()}"),
            ServiceLifetime.Singleton
        );
    }
}