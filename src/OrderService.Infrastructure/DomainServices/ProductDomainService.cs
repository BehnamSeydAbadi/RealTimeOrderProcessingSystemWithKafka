using OrderService.Domain.DomainService;

namespace OrderService.Infrastructure.DomainServices;

public class ProductDomainService : IProductDomainService
{
    public async Task<Guid[]> FilterOutValidProductIdsAsync(Guid[] productIds)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}