namespace OrderService.Domain.Order;

public interface IOrderRepository
{
    Task<Order?> GetAsync(Guid id);
}