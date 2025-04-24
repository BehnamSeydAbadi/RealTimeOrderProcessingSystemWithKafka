using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class ProductIsRequiredException() : AbstractException(message: "Product is required");