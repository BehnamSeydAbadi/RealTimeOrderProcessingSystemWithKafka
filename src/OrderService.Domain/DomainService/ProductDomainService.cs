namespace OrderService.Domain.DomainService;

internal class ProductDomainService : IProductDomainService
{
    public async Task<Guid[]> FilterOutValidProductIdsAsync(Guid[] productIds)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}