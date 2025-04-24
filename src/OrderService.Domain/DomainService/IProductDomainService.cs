namespace OrderService.Domain.DomainService;

public interface IProductDomainService
{
    Task<int[]> FilterOutValidProductIdsAsync(int[] productIds);
}