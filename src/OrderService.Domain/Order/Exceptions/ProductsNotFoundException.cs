using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class ProductsNotFoundException(int[] productIds)
    : AbstractException(message: $"Product ids: {string.Join(", ", productIds)} was not found");