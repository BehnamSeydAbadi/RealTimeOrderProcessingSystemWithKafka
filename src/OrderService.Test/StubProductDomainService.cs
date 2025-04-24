using OrderService.Domain.DomainService;

namespace OrderService.Test;

public class StubProductDomainService : IProductDomainService
{
    private int[] _validProductIds = [];

    private StubProductDomainService()
    {
    }

    public static StubProductDomainService New() => new();

    public StubProductDomainService WithValidProductIds(params int[] validProductIds)
    {
        _validProductIds = validProductIds;
        return this;
    }

    public async Task<int[]> FilterOutValidProductIdsAsync(int[] productIds)
    {
        await Task.CompletedTask;
        return _validProductIds;
    }
}