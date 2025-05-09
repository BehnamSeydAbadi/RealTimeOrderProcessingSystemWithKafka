using System.Text.Json;
using InventoryService.Application.Command.Inventory.Services;
using InventoryService.Infrastructure;
using InventoryService.Infrastructure.InboxMessages;
using InventoryService.Infrastructure.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.WebApi.BackgroundServices;

public class OrderPlacedSubscriberBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OutboxInboxDbContext>();

        var inboxMessages = await dbContext.Set<InboxMessageEntity>()
            .Where(im => im.ProcessedOn == null && im.Name == nameof(OrderPlacedIntegrationEvent))
            .OrderBy(im => im.ProcessedOn).Take(10).ToArrayAsync(stoppingToken);

        var orderPlacementApplicationService = scope.ServiceProvider
            .GetRequiredService<IOrderPlacementApplicationService>();

        foreach (var inboxMessage in inboxMessages)
        {
            var @event = JsonSerializer.Deserialize<OrderPlacedIntegrationEvent>(inboxMessage.Payload)!;

            await orderPlacementApplicationService.HandleAsync(@event.ProductIds, stoppingToken);

            inboxMessage.ProcessedOn = DateTime.UtcNow;
            dbContext.Set<InboxMessageEntity>().Update(inboxMessage);
        }

        await dbContext.SaveChangesAsync(stoppingToken);

        await Task.Delay(millisecondsDelay: 1000, stoppingToken);
    }
}