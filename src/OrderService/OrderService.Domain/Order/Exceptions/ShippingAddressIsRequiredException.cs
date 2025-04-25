using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class ShippingAddressIsRequiredException() : AbstractException(message: "Shipping address is required");