namespace OrderService.Domain.DomainService;

internal class ProductDomainService : IProductDomainService
{
    public async Task<int[]> FilterOutValidProductIdsAsync(int[] productIds)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}