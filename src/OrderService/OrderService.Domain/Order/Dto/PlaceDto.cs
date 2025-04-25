namespace OrderService.Domain.Order.Dto;

public record PlaceDto
{
    public Guid CustomerId { get; set; }
    public Guid[] ProductIds { get; set; } = [];
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string? OptionalNote { get; set; }
}