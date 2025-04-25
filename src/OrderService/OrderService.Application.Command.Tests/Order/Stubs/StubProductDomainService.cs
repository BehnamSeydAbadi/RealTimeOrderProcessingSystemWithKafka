using OrderService.Domain.DomainService;

namespace OrderService.Application.Command.Tests.Order.Stubs;

public class StubProductDomainService : IProductDomainService
{
    private Guid[] _validProductIds = [];

    private StubProductDomainService()
    {
    }

    public async Task<Guid[]> FilterOutValidProductIdsAsync(Guid[] productIds)
    {
        await Task.CompletedTask;
        return _validProductIds;
    }

    public static StubProductDomainService New() => new();

    public StubProductDomainService WithValidProductIds(Guid[] validProductIds)
    {
        _validProductIds = validProductIds;
        return this;
    }
}