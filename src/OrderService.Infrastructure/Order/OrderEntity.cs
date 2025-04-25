using OrderService.Domain.Order;

namespace OrderService.Infrastructure.Order;

public class OrderEntity
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid[] ProductIds { get; private set; } = [];
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? OptionalNote { get; private set; }
    public DateTime PlacedAt { get; private set; }
}