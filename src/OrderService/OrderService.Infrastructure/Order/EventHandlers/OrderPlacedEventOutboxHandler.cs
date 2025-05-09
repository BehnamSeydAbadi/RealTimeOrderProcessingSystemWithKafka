using System.Text.Json;
using Confluent.Kafka;
using Mediator;
using OrderService.Domain.Order.Events;
using OrderService.Infrastructure.OutboxMessages;

namespace OrderService.Infrastructure.Order.EventHandlers;

public class OrderPlacedEventOutboxHandler(OutboxInboxDbContext inboxDbContext) : INotificationHandler<OrderPlacedEvent>
{
    public async ValueTask Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new OrderPlacedIntegrationEvent
        {
            ProductIds = notification.ProductIds,
        };

        var outbox = new OutboxMessageEntity
        {
            Id = Guid.NewGuid(),
            Name = nameof(OrderPlacedIntegrationEvent),
            Payload = JsonSerializer.Serialize(integrationEvent),
            OccurredOn = DateTime.UtcNow
        };

        inboxDbContext.Set<OutboxMessageEntity>().Add(outbox);

        await inboxDbContext.SaveChangesAsync(cancellationToken);
    }
}