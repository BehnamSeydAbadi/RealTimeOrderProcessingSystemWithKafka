namespace OrderService.Domain.DomainService;

public interface ICustomerDomainService
{
    Task<bool> IsCustomerExistsAsync(int customerId);
}