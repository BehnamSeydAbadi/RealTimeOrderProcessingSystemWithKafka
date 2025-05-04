using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Events;

public record InventoryRegisteredEvent(
    Guid Id,
    string Code,
    string Name,
    string Location,
    string Description,
    DateTime RegisteredAt
) : AbstractDomainEvent;