namespace InventoryService.Infrastructure.IntegrationEvents;

public record OrderPlacedIntegrationEvent
{
    public Guid[] ProductIds { get; set; } = [];
}