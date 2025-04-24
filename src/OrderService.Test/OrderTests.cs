using FluentAssertions;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Test;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public async Task PlaceOrder_Should_SetStatusToPending()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        var order = await Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(CustomerId: 1, ProductIds: [1], ShippingAddress: "1", PaymentMethod: "payment")
        );

        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact(DisplayName =
        "When an order gets placed without any product, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutAnyProduct_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        var action = () => Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(CustomerId: 1, ProductIds: [], ShippingAddress: "1", PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<ProductIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without shipping address, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutShippingAddress_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        var action = () => Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(CustomerId: 1, ProductIds: [1], ShippingAddress: string.Empty, PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<ShippingAddressIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without payment method, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutPaymentMethod_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        var action = () => Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(CustomerId: 1, ProductIds: [1], ShippingAddress: "1", PaymentMethod: string.Empty)
        );

        await action.Should().ThrowExactlyAsync<PaymentMethodIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with correct data, Then it should be placed with correct data successfully")]
    public async Task PlaceOrder_WithCorrectData_ShouldBePlacedSuccessfully()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        var order = await Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(
                CustomerId: 1, ProductIds: [1], ShippingAddress: "1", PaymentMethod: "payment", OptionalNote: "notes"
            )
        );

        order.CustomerId.Should().Be(1);
        order.Status.Should().Be(OrderStatus.Pending);
        order.ProductIds[0].Should().Be(1);
        order.ShippingAddress.Should().Be("1");
        order.PaymentMethod.Should().Be("payment");
        order.OptionalNote.Should().Be("notes");
    }

    [Fact(DisplayName =
        "When an order gets placed with invalid customer id, Then an exception should be thrown")]
    public async Task PlaceOrder_WithInvalidCustomerId_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(false);

        var action = () => Order.PlaceAsync(
            customerDomainService,
            new PlaceDto(CustomerId: 1, ProductIds: [1], ShippingAddress: "1", PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<CustomerNotFoundException>();
    }
}