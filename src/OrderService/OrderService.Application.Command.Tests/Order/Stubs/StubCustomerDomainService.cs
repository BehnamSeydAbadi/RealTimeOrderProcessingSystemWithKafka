using OrderService.Domain.DomainService;

namespace OrderService.Application.Command.Tests.Order.Stubs;

public class StubCustomerDomainService : ICustomerDomainService
{
    private bool _isCustmerExists;

    private StubCustomerDomainService()
    {
    }

    public static StubCustomerDomainService New() => new();

    public async Task<bool> IsCustomerExistsAsync(Guid customerId)
    {
        await Task.CompletedTask;
        return _isCustmerExists;
    }

    public StubCustomerDomainService WithIsCustmerExistsValue(bool isCustmerExists)
    {
        _isCustmerExists = isCustmerExists;
        return this;
    }
}