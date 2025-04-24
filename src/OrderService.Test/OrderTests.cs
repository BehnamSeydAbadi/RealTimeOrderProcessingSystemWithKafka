using FluentAssertions;
using OrderService.Domain;

namespace OrderService.Test;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public void PlaceOrder_Should_SetStatusToPending()
    {
        var order = Order.Place();
        order.Status.Should().Be(OrderStatus.Pending);
    }
}