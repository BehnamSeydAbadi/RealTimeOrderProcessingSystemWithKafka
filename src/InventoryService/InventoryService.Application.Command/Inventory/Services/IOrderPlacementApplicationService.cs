namespace InventoryService.Application.Command.Inventory.Services;

public interface IOrderPlacementApplicationService
{
    Task HandleAsync(Guid[] productIds, CancellationToken stoppingToken);
}