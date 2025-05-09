using InventoryService.Domain.Common;

namespace InventoryService.Domain.Inventory.Exceptions;

public class ProductNotFoundException() : AbstractException(message: "Product not found");