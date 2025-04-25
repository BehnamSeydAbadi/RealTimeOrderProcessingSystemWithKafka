using FluentAssertions;
using OrderService.Domain.Order;
using OrderService.Domain.Order.Dto;
using OrderService.Domain.Order.Exceptions;

namespace OrderService.Domain.Tests.Order;

public class OrderTests
{
    [Fact(DisplayName =
        "When an order gets placed, Then it's status should be Pending")]
    public void PlaceOrder_Should_SetStatusToPending()
    {
        var order = Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [Guid.NewGuid()],
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            }
        );

        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact(DisplayName =
        "When an order gets placed without any product, Then an exception should be thrown")]
    public void PlaceOrder_WithoutAnyProduct_ShouldThrowException()
    {
        var action = () => Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [],
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            }
        );

        action.Should().ThrowExactly<ProductIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without shipping address, Then an exception should be thrown")]
    public void PlaceOrder_WithoutShippingAddress_ShouldThrowException()
    {
        var action = () => Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [Guid.NewGuid()],
                ShippingAddress = string.Empty,
                PaymentMethod = "payment",
                OptionalNote = null
            }
        );

        action.Should().ThrowExactly<ShippingAddressIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed without payment method, Then an exception should be thrown")]
    public void PlaceOrder_WithoutPaymentMethod_ShouldThrowException()
    {
        var action = () => Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [Guid.NewGuid()],
                ShippingAddress = "1",
                PaymentMethod = string.Empty,
                OptionalNote = null
            }
        );

        action.Should().ThrowExactly<PaymentMethodIsRequiredException>();
    }

    [Fact(DisplayName =
        "When an order gets placed with correct data, Then it should be placed with correct data successfully")]
    public void PlaceOrder_WithCorrectData_ShouldBePlacedSuccessfully()
    {
        Guid[] validProductIds = [Guid.NewGuid()];

        var customerId = Guid.NewGuid();

        var order = Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = customerId,
                ProductIds = validProductIds,
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = "notes"
            }
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
        "When an order gets placed with duplicate products, Then an exception should be thrown")]
    public void PlaceOrder_WithDuplicateProducts_ShouldThrowException()
    {
        Guid[] validProductIds = [Guid.NewGuid()];

        var action = () => Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [validProductIds[0], validProductIds[0]],
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            }
        );

        action.Should().ThrowExactly<DuplicateProductException>();
    }

    [Fact(DisplayName =
        "When an order gets placed, Then it's date and time placement should be set")]
    public void PlaceOrder_ShouldTimestampBeSet()
    {
        var order = Domain.Order.Order.Place(
            new PlaceDto
            {
                CustomerId = Guid.NewGuid(),
                ProductIds = [Guid.NewGuid()],
                ShippingAddress = "1",
                PaymentMethod = "payment",
                OptionalNote = null
            }
        );

        order.PlacedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}