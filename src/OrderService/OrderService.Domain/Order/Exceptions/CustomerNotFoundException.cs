using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class CustomerNotFoundException() : AbstractException(message: "Customer not found");