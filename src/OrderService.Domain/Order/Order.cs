using OrderService.Domain.Common;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order
{
    public static Order Place(
        int[] productIds, string shippingAddress, string paymentMethod, string? optionalNote = null
    )
    {
        Guard.Assert<ProductIsRequiredException>(productIds.Length == 0);
        Guard.Assert<ShippingAddressIsRequiredException>(string.IsNullOrWhiteSpace(shippingAddress));
        Guard.Assert<PaymentMethodIsRequiredException>(string.IsNullOrWhiteSpace(paymentMethod));

        return new Order
        {
            Status = OrderStatus.Pending,
            ProductIds = productIds,
            ShippingAddress = shippingAddress,
            PaymentMethod = paymentMethod,
            OptionalNote = optionalNote
        };
    }

    public OrderStatus Status { get; private set; }
    public int[] ProductIds { get; private set; } = [];
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? OptionalNote { get; private set; }
}