using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Exceptions;

public class InventoryNotFoundException() : AbstractException(message: "Inventory not found");