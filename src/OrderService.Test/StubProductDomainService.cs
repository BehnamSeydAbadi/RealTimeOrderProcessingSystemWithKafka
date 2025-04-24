using OrderService.Domain.DomainService;

namespace OrderService.Test;

public class StubProductDomainService : IProductDomainService
{
    private Guid[] _validProductIds = [];

    private StubProductDomainService()
    {
    }

    public static StubProductDomainService New() => new();

    public StubProductDomainService WithValidProductIds(params Guid[] validProductIds)
    {
        _validProductIds = validProductIds;
        return this;
    }

    public async Task<Guid[]> FilterOutValidProductIdsAsync(Guid[] productIds)
    {
        await Task.CompletedTask;
        return _validProductIds;
    }
}