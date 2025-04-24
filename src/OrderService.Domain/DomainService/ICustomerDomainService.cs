namespace OrderService.Domain.DomainService;

public interface ICustomerDomainService
{
    Task<bool> IsCustomerExistsAsync(int customerId);
}

public class CustomerDomainService : ICustomerDomainService
{
    public async Task<bool> IsCustomerExistsAsync(int customerId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}