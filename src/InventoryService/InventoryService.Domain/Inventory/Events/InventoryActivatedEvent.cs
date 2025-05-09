using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Events;

public record InventoryActivatedEvent(Guid Id) : AbstractDomainEvent;