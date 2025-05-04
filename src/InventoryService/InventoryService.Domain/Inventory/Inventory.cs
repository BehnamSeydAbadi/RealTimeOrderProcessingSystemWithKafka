using InventoryService.Domain.Common;
using InventoryService.Domain.Inventory.Dto;
using InventoryService.Domain.Inventory.Events;

namespace InventoryService.Domain.Inventory;

public class Inventory : AbstractAggregateRoot
{
    public static Inventory Register(RegisterDto dto)
    {
        var inventoryRegisteredEvent = new InventoryRegisteredEvent(
            Guid.NewGuid(), dto.Code, dto.Name, dto.Location, dto.Description, DateTime.UtcNow
        );

        var inventory = new Inventory();

        inventory.EnqueueDomainEvent(inventoryRegisteredEvent);
        inventory.Mutate(inventoryRegisteredEvent);

        return inventory;
    }

    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    protected override void When(AbstractDomainEvent domainEvent) => On((dynamic)domainEvent);

    private void On(InventoryRegisteredEvent domainEvent)
    {
        this.Id = domainEvent.Id;
        this.Code = domainEvent.Code;
        this.Name = domainEvent.Name;
        this.Location = domainEvent.Location;
        this.Description = domainEvent.Description;
        this.RegisteredAt = domainEvent.RegisteredAt;
    }
}