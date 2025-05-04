using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Exceptions;

public class DuplicateInventoryCodeException() : AbstractException(message: "Duplicate inventory code");