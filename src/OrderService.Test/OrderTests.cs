using FluentAssertions;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Test;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public void PlaceOrder_Should_SetStatusToPending()
    {
        var order = Order.Place(
            productIds: [1], shippingAddress: "1", paymentMethod: "payment"
        );

        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact(DisplayName =
        "When an order gets placed without any product, Then an exception should be thrown")]
    public void PlaceOrder_WithoutAnyProduct_ShouldThrowException()
    {
        var action = () => Order.Place(productIds: [], shippingAddress: "1", paymentMethod: "payment");
        action.Should().ThrowExactly<ProductIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without shipping address, Then an exception should be thrown")]
    public void PlaceOrder_WithoutShippingAddress_ShouldThrowException()
    {
        var action = () => Order.Place(productIds: [1], shippingAddress: string.Empty, paymentMethod: "payment");
        action.Should().ThrowExactly<ShippingAddressIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without payment method, Then an exception should be thrown")]
    public void PlaceOrder_WithoutPaymentMethod_ShouldThrowException()
    {
        var action = () => Order.Place(productIds: [1], shippingAddress: "1", paymentMethod: string.Empty);
        action.Should().ThrowExactly<PaymentMethodIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with correct data, Then it should be placed with correct data successfully")]
    public void PlaceOrder_WithCorrectData_ShouldBePlacedSuccessfully()
    {
        var order = Order.Place(
            productIds: [1], shippingAddress: "1", paymentMethod: "payment", optionalNote: "notes"
        );

        order.Status.Should().Be(OrderStatus.Pending);
        order.ProductIds[0].Should().Be(1);
        order.ShippingAddress.Should().Be("1");
        order.PaymentMethod.Should().Be("payment");
        order.OptionalNote.Should().Be("notes");
    }
}