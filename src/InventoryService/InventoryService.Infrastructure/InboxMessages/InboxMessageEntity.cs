namespace InventoryService.Infrastructure.InboxMessages;

public class InboxMessageEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Payload { get; set; }
    public DateTime ReceivedOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }
}