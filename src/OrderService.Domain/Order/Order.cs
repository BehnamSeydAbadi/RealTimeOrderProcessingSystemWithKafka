using OrderService.Domain.Common;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order
{
    public static async Task<Order> PlaceAsync(
        ICustomerDomainService customerDomainService,
        int customerId, int[] productIds, string shippingAddress, string paymentMethod, string? optionalNote = null
    )
    {
        var isCustomerExists = await customerDomainService.IsCustomerExistsAsync(customerId);
        Guard.Assert<CustomerNotFoundException>(isCustomerExists is false);

        Guard.Assert<ProductIsRequiredException>(productIds.Length == 0);
        Guard.Assert<ShippingAddressIsRequiredException>(string.IsNullOrWhiteSpace(shippingAddress));
        Guard.Assert<PaymentMethodIsRequiredException>(string.IsNullOrWhiteSpace(paymentMethod));

        return new Order
        {
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            ProductIds = productIds,
            ShippingAddress = shippingAddress,
            PaymentMethod = paymentMethod,
            OptionalNote = optionalNote
        };
    }

    public int CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public int[] ProductIds { get; private set; } = [];
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? OptionalNote { get; private set; }
}