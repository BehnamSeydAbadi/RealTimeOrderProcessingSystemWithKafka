using FluentAssertions;
using OrderService.Domain;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Test;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public void PlaceOrder_Should_SetStatusToPending()
    {
        var order = Order.Place(productIds: [1]);
        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact(DisplayName =
        "When an order gets placed without any product, Then an exception should be thrown")]
    public void PlaceOrder_WithoutAnyProduct_ShouldThrowException()
    {
        var action = () => Order.Place(productIds: []);
        action.Should().ThrowExactly<ProductIsRequiredException>();
    }
}