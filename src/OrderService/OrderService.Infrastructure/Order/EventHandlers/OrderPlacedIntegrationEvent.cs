namespace OrderService.Infrastructure.Order.EventHandlers;

public record OrderPlacedIntegrationEvent
{
    public Guid[] ProductIds { get; set; } = [];
}