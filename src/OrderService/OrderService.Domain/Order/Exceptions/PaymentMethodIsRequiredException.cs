using OrderService.Domain.Common;

namespace OrderService.Domain.Order.Exceptions;

public class PaymentMethodIsRequiredException() : AbstractException(message: "Payment method is required");