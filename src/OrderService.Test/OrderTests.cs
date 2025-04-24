using FluentAssertions;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Events;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Test;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public async Task PlaceOrder_Should_SetStatusToPending()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var order = await Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), validProductIds, ShippingAddress: "1", PaymentMethod: "payment"
            )
        );

        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact(DisplayName =
        "When an order gets placed without any product, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutAnyProduct_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);
        var productDomainService = StubProductDomainService.New();

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(CustomerId: Guid.NewGuid(), ProductIds: [], ShippingAddress: "1", PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<ProductIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without shipping address, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutShippingAddress_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), validProductIds,
                ShippingAddress: string.Empty, PaymentMethod: "payment"
            )
        );

        await action.Should().ThrowExactlyAsync<ShippingAddressIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without payment method, Then an exception should be thrown")]
    public async Task PlaceOrder_WithoutPaymentMethod_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), validProductIds, ShippingAddress: "1", PaymentMethod: string.Empty
            )
        );

        await action.Should().ThrowExactlyAsync<PaymentMethodIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with correct data, Then it should be placed with correct data successfully")]
    public async Task PlaceOrder_WithCorrectData_ShouldBePlacedSuccessfully()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var customerId = Guid.NewGuid();

        var order = await Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                customerId, validProductIds, ShippingAddress: "1",
                PaymentMethod: "payment", OptionalNote: "notes"
            )
        );

        order.Id.Should().NotBe(Guid.Empty);
        order.CustomerId.Should().Be(customerId);
        order.Status.Should().Be(OrderStatus.Pending);
        order.ProductIds[0].Should().Be(validProductIds[0]);
        order.ShippingAddress.Should().Be("1");
        order.PaymentMethod.Should().Be("payment");
        order.OptionalNote.Should().Be("notes");
        order.PlacedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName =
        "When an order gets placed with invalid customer id, Then an exception should be thrown")]
    public async Task PlaceOrder_WithInvalidCustomerId_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(false);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(CustomerId: Guid.NewGuid(), validProductIds, ShippingAddress: "1", PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<CustomerNotFoundException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with duplicate products, Then an exception should be thrown")]
    public async Task PlaceOrder_WithDuplicateProducts_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), ProductIds: [validProductIds[0], validProductIds[0]],
                ShippingAddress: "1", PaymentMethod: "payment"
            )
        );

        await action.Should().ThrowExactlyAsync<DuplicateProductException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with invalid product ids, Then an exception should be thrown")]
    public async Task PlaceOrder_WithInvalidProductIds_ShouldThrowException()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        Guid[] invalidProductIds = [Guid.NewGuid(), Guid.NewGuid()];

        var action = () => Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(),
                ProductIds: [validProductIds[0], invalidProductIds[0], invalidProductIds[1]],
                ShippingAddress: "1", PaymentMethod: "payment")
        );

        await action.Should().ThrowExactlyAsync<ProductsNotFoundException>(
            because: $"Product ids: {invalidProductIds[0]}, {invalidProductIds[1]} was not found"
        );
    }

    [Fact(DisplayName =
        "When an order gets placed, Then it's date and time placement should be set")]
    public async Task PlaceOrder_ShouldTimestampBeSet()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var order = await Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), ProductIds: validProductIds,
                ShippingAddress: "1", PaymentMethod: "payment"
            )
        );

        order.PlacedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName =
        "When an order gets placed, Then an event should be queued")]
    public async Task PlaceOrder_ShouldEnqueueEvent()
    {
        var customerDomainService = StubCustomerDomainService.New().WithIsCustmerExistsValue(true);

        Guid[] validProductIds = [Guid.NewGuid()];
        var productDomainService = StubProductDomainService.New().WithValidProductIds(validProductIds);

        var order = await Order.PlaceAsync(
            customerDomainService, productDomainService,
            new PlaceDto(
                CustomerId: Guid.NewGuid(), ProductIds: validProductIds,
                ShippingAddress: "1", PaymentMethod: "payment"
            )
        );

        order.GetDomainEventsQueue().Should().NotBeEmpty();

        var domainEvent = order.GetDomainEventsQueue().Dequeue();
        domainEvent.Should().BeOfType<OrderPlacedEvent>();

        var orderPlacedEvent = (OrderPlacedEvent)domainEvent;
        orderPlacedEvent.Id.Should().Be(order.Id);
        orderPlacedEvent.CustomerId.Should().Be(order.CustomerId);
        orderPlacedEvent.Status.Should().Be(order.Status);
        orderPlacedEvent.ProductIds.Should().BeEquivalentTo(order.ProductIds);
        orderPlacedEvent.ShippingAddress.Should().Be(order.ShippingAddress);
        orderPlacedEvent.PaymentMethod.Should().Be(order.PaymentMethod);
        orderPlacedEvent.OptionalNote.Should().Be(order.OptionalNote);
        orderPlacedEvent.PlacedAt.Should().Be(order.PlacedAt);
    }
}