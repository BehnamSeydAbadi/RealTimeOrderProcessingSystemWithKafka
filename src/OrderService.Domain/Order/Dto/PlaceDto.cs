namespace OrderService.Domain.Order.Dto;

public record PlaceDto(
    int CustomerId,
    int[] ProductIds,
    string ShippingAddress,
    string PaymentMethod,
    string? OptionalNote = null
);