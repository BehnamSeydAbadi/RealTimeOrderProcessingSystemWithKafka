using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Events;

public record OrderPlacedEvent : AbstractDomainEvent
{
    public OrderPlacedEvent(
        Guid id, Guid customerId, OrderStatus status, Guid[] productIds, string shippingAddress,
        string paymentMethod, string? optionalNote, DateTime placedAt
    )
    {
        Id = id;
        CustomerId = customerId;
        Status = status;
        ProductIds = productIds;
        ShippingAddress = shippingAddress;
        PaymentMethod = paymentMethod;
        OptionalNote = optionalNote;
        PlacedAt = placedAt;
    }

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public Guid[] ProductIds { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string? OptionalNote { get; set; }
    public DateTime PlacedAt { get; set; }
}