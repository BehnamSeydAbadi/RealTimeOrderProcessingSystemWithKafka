using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.DomainService;

namespace OrderService.Domain;

public class DomainBootstrapper
{
    public static void Run(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICustomerDomainService, CustomerDomainService>();
        serviceCollection.AddTransient<IProductDomainService, ProductDomainService>();
    }
}