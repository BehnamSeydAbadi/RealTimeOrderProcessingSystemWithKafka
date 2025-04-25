using FluentAssertions;
using Mediator;
using OrderService.Domain.Order.Events;

namespace OrderService.Application.Command.Tests.Order.Mock;

public class MockPublisher : IPublisher
{
    private INotification _notification;
    private INotification _publishedNotification;

    private MockPublisher()
    {
    }

    public static MockPublisher New() => new();

    public MockPublisher WithNotification(INotification notification)
    {
        _notification = notification;
        return this;
    }

    public async ValueTask Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = new()) where TNotification : INotification
    {
        _publishedNotification = notification;
        await ValueTask.CompletedTask;
    }

    public async ValueTask Publish(object notification, CancellationToken cancellationToken = new())
    {
        _publishedNotification = (notification as INotification)!;
        await ValueTask.CompletedTask;
    }

    public void VerifyOrderPlacedEvent()
    {
        var orderPlacedEvent = (_notification as OrderPlacedEvent)!;
        var publishedOrderPlacedEvent = (_publishedNotification as OrderPlacedEvent)!;

        publishedOrderPlacedEvent.Id.Should().NotBe(Guid.Empty);
        publishedOrderPlacedEvent.CustomerId.Should().Be(orderPlacedEvent.CustomerId);
        publishedOrderPlacedEvent.Status.Should().Be(orderPlacedEvent.Status);
        publishedOrderPlacedEvent.ProductIds.Should().BeEquivalentTo(orderPlacedEvent.ProductIds);
        publishedOrderPlacedEvent.ShippingAddress.Should().Be(orderPlacedEvent.ShippingAddress);
        publishedOrderPlacedEvent.PaymentMethod.Should().Be(orderPlacedEvent.PaymentMethod);
        publishedOrderPlacedEvent.OptionalNote.Should().Be(orderPlacedEvent.OptionalNote);
        publishedOrderPlacedEvent.PlacedAt.Should().BeCloseTo(orderPlacedEvent.PlacedAt, TimeSpan.FromSeconds(1));
    }
}