using OrderService.Domain.Common;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Events;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order : AbstractAggregateRoot
{
    public static Order Place(PlaceDto dto)
    {
        Guard.Assert<ProductIsRequiredException>(dto.ProductIds.Length == 0);
        Guard.Assert<DuplicateProductException>(dto.ProductIds.Length > dto.ProductIds.Distinct().Count());
        Guard.Assert<ShippingAddressIsRequiredException>(string.IsNullOrWhiteSpace(dto.ShippingAddress));
        Guard.Assert<PaymentMethodIsRequiredException>(string.IsNullOrWhiteSpace(dto.PaymentMethod));

        var order = new Order();

        var orderPlacedEvent = new OrderPlacedEvent(
            id: Guid.NewGuid(), dto.CustomerId, OrderStatus.Pending, dto.ProductIds,
            dto.ShippingAddress, dto.PaymentMethod, dto.OptionalNote, DateTime.UtcNow
        );

        order.EnqueueDomainEvent(orderPlacedEvent);
        order.Mutate(orderPlacedEvent);

        return order;
    }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid[] ProductIds { get; private set; } = [];
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? OptionalNote { get; private set; }
    public DateTime PlacedAt { get; private set; }

    protected override void When(AbstractDomainEvent domainEvent) => On((dynamic)domainEvent);

    private void On(OrderPlacedEvent domainEvent)
    {
        this.Id = domainEvent.Id;
        this.CustomerId = domainEvent.CustomerId;
        this.Status = domainEvent.Status;
        this.ProductIds = domainEvent.ProductIds;
        this.ShippingAddress = domainEvent.ShippingAddress;
        this.PaymentMethod = domainEvent.PaymentMethod;
        this.OptionalNote = domainEvent.OptionalNote;
        this.PlacedAt = domainEvent.PlacedAt;
    }
}