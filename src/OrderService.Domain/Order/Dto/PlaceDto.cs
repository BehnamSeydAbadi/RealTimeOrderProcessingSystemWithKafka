namespace OrderService.Domain.Order.Dto;

public record PlaceDto(
    Guid CustomerId,
    Guid[] ProductIds,
    string ShippingAddress,
    string PaymentMethod,
    string? OptionalNote = null
);