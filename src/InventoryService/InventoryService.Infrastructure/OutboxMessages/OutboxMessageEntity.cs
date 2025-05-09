namespace InventoryService.Infrastructure.OutboxMessages;

public class OutboxMessageEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Payload { get; set; }
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }
}