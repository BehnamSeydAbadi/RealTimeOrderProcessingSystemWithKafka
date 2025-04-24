using OrderService.Domain.Common;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order
{
    public static Order Place(int[] productIds)
    {
        Guard.Assert<ProductIsRequiredException>(productIds.Length == 0);

        return new Order
        {
            Status = OrderStatus.Pending,
            ProductIds = productIds
        };
    }

    public OrderStatus Status { get; private set; }
    public int[] ProductIds { get; private set; } = [];
}