using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Events;

public record ProductRemovedFromInventoryEvent(Guid Id, Guid ProductId) : AbstractDomainEvent;