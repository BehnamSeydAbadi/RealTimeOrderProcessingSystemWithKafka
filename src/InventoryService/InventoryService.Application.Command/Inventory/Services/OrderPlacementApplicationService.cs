using InventoryService.Domain.Inventory;
using Mediator;

namespace InventoryService.Application.Command.Inventory.Services;

internal class OrderPlacementApplicationService(IInventoryRepository inventoryRepository, IPublisher publisher)
    : IOrderPlacementApplicationService
{
    public async Task HandleAsync(Guid[] productIds, CancellationToken stoppingToken)
    {
        var inventoryModels = await inventoryRepository.GetInventoriesByProductIdsAsync<InventoryModel>(
            productIds, stoppingToken
        );

        foreach (var productId in productIds)
        {
            foreach (var inventoryModel in inventoryModels)
            {
                if (inventoryModel.AnyProduct(productId) is false) continue;

                inventoryModel.RemoveProduct(productId);
                await publisher.Publish(inventoryModel.GetDomainEventsQueue(), stoppingToken);
            }
        }
    }
}