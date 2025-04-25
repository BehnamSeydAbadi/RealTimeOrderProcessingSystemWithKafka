using OrderService.Domain.Order;

namespace OrderService.Infrastructure.Order;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public Guid[] ProductIds { get; set; } = [];
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string? OptionalNote { get; set; }
    public DateTime PlacedAt { get; set; }
}