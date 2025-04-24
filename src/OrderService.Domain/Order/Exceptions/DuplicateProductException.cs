using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class DuplicateProductException() : AbstractException(message: "Products are duplicated");