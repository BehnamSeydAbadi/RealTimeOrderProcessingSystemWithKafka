using OrderService.Application.Command.Order;
using OrderService.Application.Command.Tests.Order.Stubs;
using OrderService.Domain.Order.Exceptions;
using FluentAssertions;
using OrderService.Domain.Order.Dto;
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

        var commandHandler = new PlaceOrderCommandHandler(customerDomainService, productDomainService);

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

        var commandHandler = new PlaceOrderCommandHandler(customerDomainService, productDomainService);

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

    // [Fact(DisplayName =
    //     "When an order gets placed, Then an event should be queued")]
    // public async Task PlaceOrder_ShouldEnqueueEvent()
    // {
    //     var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);
    //
    //     Guid[] validProductIds = [Guid.NewGuid()];
    //     var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);
    //
    //     var order = await Order.Place(
    //         customerDomainService, productDomainService,
    //         new PlaceDto(
    //             CustomerId: Guid.NewGuid(), ProductIds: validProductIds,
    //             ShippingAddress: "1", PaymentMethod: "payment"
    //         )
    //     );
    //
    //     order.GetDomainEventsQueue().Should().NotBeEmpty();
    //
    //     var domainEvent = order.GetDomainEventsQueue().Dequeue();
    //     domainEvent.Should().BeOfType<OrderPlacedEvent>();
    //
    //     var orderPlacedEvent = (OrderPlacedEvent)domainEvent;
    //     orderPlacedEvent.Id.Should().Be(order.Id);
    //     orderPlacedEvent.CustomerId.Should().Be(order.CustomerId);
    //     orderPlacedEvent.Status.Should().Be(order.Status);
    //     orderPlacedEvent.ProductIds.Should().BeEquivalentTo(order.ProductIds);
    //     orderPlacedEvent.ShippingAddress.Should().Be(order.ShippingAddress);
    //     orderPlacedEvent.PaymentMethod.Should().Be(order.PaymentMethod);
    //     orderPlacedEvent.OptionalNote.Should().Be(order.OptionalNote);
    //     orderPlacedEvent.PlacedAt.Should().Be(order.PlacedAt);
    // }
}