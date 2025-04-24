using OrderService.Domain.Common;
using OrderService.Domain.DomainService;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Order;

public class Order
{
    public static async Task<Order> PlaceAsync(
        ICustomerDomainService customerDomainService, IProductDomainService productDomainService, PlaceDto dto
    )
    {
        Guard.Assert<ProductIsRequiredException>(dto.ProductIds.Length == 0);
        Guard.Assert<DuplicateProductException>(dto.ProductIds.Length > dto.ProductIds.Distinct().Count());
        Guard.Assert<ShippingAddressIsRequiredException>(string.IsNullOrWhiteSpace(dto.ShippingAddress));
        Guard.Assert<PaymentMethodIsRequiredException>(string.IsNullOrWhiteSpace(dto.PaymentMethod));

        var isCustomerExists = await customerDomainService.IsCustomerExistsAsync(dto.CustomerId);
        Guard.Assert<CustomerNotFoundException>(isCustomerExists is false);

        var validProductIds = await productDomainService.FilterOutValidProductIdsAsync(dto.ProductIds);
        var invalidProductIds = dto.ProductIds.Except(validProductIds).ToArray();
        Guard.Assert(invalidProductIds.Length > 0, new ProductsNotFoundException(invalidProductIds));

        return new Order
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