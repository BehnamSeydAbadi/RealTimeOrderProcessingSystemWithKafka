using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Order;

namespace OrderService.Infrastructure.Order;

public class OrderRepository : IOrderRepository
{
    private readonly OrderServiceDbContext _dbContext;

    public OrderRepository(OrderServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Order.Order?> GetAsync(Guid id)
    {
        var orderEntity = await _dbContext.Set<OrderEntity>().FirstOrDefaultAsync(o => o.Id == id);

        return orderEntity?.Adapt<Domain.Order.Order>();
    }
}