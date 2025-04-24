using OrderService.Domain.Common;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order
{
    public static async Task<Order> PlaceAsync(ICustomerDomainService customerDomainService, PlaceDto dto)
    {
        var isCustomerExists = await customerDomainService.IsCustomerExistsAsync(dto.CustomerId);
        Guard.Assert<CustomerNotFoundException>(isCustomerExists is false);

        Guard.Assert<ProductIsRequiredException>(dto.ProductIds.Length == 0);
        Guard.Assert<ShippingAddressIsRequiredException>(string.IsNullOrWhiteSpace(dto.ShippingAddress));
        Guard.Assert<PaymentMethodIsRequiredException>(string.IsNullOrWhiteSpace(dto.PaymentMethod));

        return new Order
        {
            CustomerId = dto.CustomerId,
            Status = OrderStatus.Pending,
            ProductIds = dto.ProductIds,
            ShippingAddress = dto.ShippingAddress,
            PaymentMethod = dto.PaymentMethod,
            OptionalNote = dto.OptionalNote
        };
    }

    public int CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public int[] ProductIds { get; private set; } = [];
    public string ShippingAddress { get; private set; }
    public string PaymentMethod { get; private set; }
    public string? OptionalNote { get; private set; }
}