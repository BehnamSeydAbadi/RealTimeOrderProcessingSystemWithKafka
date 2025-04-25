using OrderService.Application.Command.Order;
using OrderService.Application.Command.Tests.Order.Stubs;
using OrderService.Domain.Order.Exceptions;
using FluentAssertions;
using OrderService.Application.Command.Tests.Order.Mock;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Events;

namespace OrderService.Application.Command.Tests.Order;

public class PlaceOrderCommandTests
{
    [Fact(DisplayName =
        "When an order gets placed with invalid customer id, Then an exception should be thrown")]
    public async Task PlaceOrder_WithInvalidCustomerId_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(false);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var commandHandler = new PlaceOrderCommandHandler(
            customerDomainService, productDomainService, StubPublisher.New()
        );

        var action = async () => await commandHandler.Handle(
            new PlaceOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = validProductIds,
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            },
            CancellationToken.None
        );

        await action.Should().ThrowExactlyAsync<CustomerNotFoundException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with invalid product ids, Then an exception should be thrown")]
    public async Task PlaceOrder_WithInvalidProductIds_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        Guid[] invalidProductIds = [Guid.NewGuid(), Guid.NewGuid()];

        var commandHandler = new PlaceOrderCommandHandler(
            customerDomainService, productDomainService, StubPublisher.New()
        );

        var action = async () => await commandHandler.Handle(
            new PlaceOrderCommand
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [validProductIds[0], invalidProductIds[0], invalidProductIds[1]],
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            },
            CancellationToken.None
        );

        await action.Should().ThrowExactlyAsync<ProductsNotFoundException>(
            because: $"Product ids: {invalidProductIds[0]}, {invalidProductIds[1]} was not found"
        );
    }

    [Fact(DisplayName =
        "When an order gets placed, Then an event should be queued")]
    public async Task PlaceOrder_ShouldEnqueueEvent()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var orderPlacedEvent = new OrderPlacedEvent(
            id: Guid.Empty, customerId: Guid.NewGuid(), OrderStatus.Pending, productIds: validProductIds,
            shippingAddress: "1", paymentMethod: "payment", optionalNote: "something", placedAt: DateTime.UtcNow
        );

        var mockPublisher = MockPublisher.New().WithNotification(orderPlacedEvent);

        var commandHandler = new PlaceOrderCommandHandler(
            customerDomainService, productDomainService, mockPublisher
        );

        await commandHandler.Handle(
            new PlaceOrderCommand
            {
                CustomerId = orderPlacedEvent.CustomerId,
                ProductIds = orderPlacedEvent.ProductIds,
                ShippingAddress = orderPlacedEvent.ShippingAddress,
                PaymentMethod = orderPlacedEvent.PaymentMethod,
                OptionalNote = orderPlacedEvent.OptionalNote
            },
            CancellationToken.None
        );

        mockPublisher.VerifyOrderPlacedEvent();
    }
}