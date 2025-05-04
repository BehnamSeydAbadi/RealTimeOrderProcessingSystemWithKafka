using InventoryService.Application.Query.Inventory.ViewModels;
using Mediator;

namespace InventoryService.Application.Query.Inventory;

public class GetInventoriesQuery : IQuery<InventoryViewModel[]>;