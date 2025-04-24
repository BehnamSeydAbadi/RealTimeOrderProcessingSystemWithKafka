namespace OrderService.Domain.DomainService;

public interface IProductDomainService
{
    Task<Guid[]> FilterOutValidProductIdsAsync(Guid[] productIds);
}