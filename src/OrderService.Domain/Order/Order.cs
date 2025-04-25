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

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = dto.CustomerId,
            Status = OrderStatus.Pending,
            ProductIds = dto.ProductIds,
            ShippingAddress = dto.ShippingAddress,
            PaymentMethod = dto.PaymentMethod,
            OptionalNote = dto.OptionalNote,
            PlacedAt = DateTime.UtcNow
        };

        order.EnqueueDomainEvent(
            new OrderPlacedEvent(
                order.Id, order.CustomerId, order.Status, order.ProductIds,
                order.ShippingAddress, order.PaymentMethod, order.OptionalNote, order.PlacedAt
            )
        );

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
}