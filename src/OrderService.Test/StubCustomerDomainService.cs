using OrderService.Domain.DomainService;

namespace OrderService.Test;

public class StubCustomerDomainService : ICustomerDomainService
{
    private bool _isCustomerExists;

    private StubCustomerDomainService()
    {
    }

    public static StubCustomerDomainService New() => new();

    public StubCustomerDomainService WithIsCustmerExistsValue(bool isCustomerExists)
    {
        _isCustomerExists = isCustomerExists;
        return this;
    }

    public async Task<bool> IsCustomerExistsAsync(Guid customerId)
    {
        await Task.CompletedTask;
        return _isCustomerExists;
    }
}