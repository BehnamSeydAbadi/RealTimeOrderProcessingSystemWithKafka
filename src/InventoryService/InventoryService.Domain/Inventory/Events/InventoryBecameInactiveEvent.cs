using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Events;

public record InventoryBecameInactiveEvent(Guid Id) : AbstractDomainEvent;