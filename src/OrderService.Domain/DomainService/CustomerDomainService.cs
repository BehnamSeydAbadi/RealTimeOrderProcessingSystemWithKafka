namespace OrderService.Domain.DomainService;

internal class CustomerDomainService : ICustomerDomainService
{
    public async Task<bool> IsCustomerExistsAsync(Guid customerId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}