using Mediator;

namespace OrderService.Application.Command.Order;

public record PlaceOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public Guid[] ProductIds { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
    public string? OptionalNote { get; set; } = null;
}