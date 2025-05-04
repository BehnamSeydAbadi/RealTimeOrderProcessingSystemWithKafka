using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Exceptions;

public class InActiveInventoryException() : AbstractException(message: "Inventory is inactive");