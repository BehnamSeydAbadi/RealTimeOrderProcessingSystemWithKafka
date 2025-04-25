using OrderService.Domain.DomainService;

namespace OrderService.Infrastructure.DomainServices;

public class CustomerDomainService : ICustomerDomainService
{
    public async Task<bool> IsCustomerExistsAsync(Guid customerId)
    {
        await Task.CompletedTask;
        return true;
    }
}