using System.Text.Json;
using Confluent.Kafka;
using InventoryService.Infrastructure.InboxMessages;
using InventoryService.Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryService.Infrastructure;

public class InboxBackgroundService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            Console.WriteLine("InboxBackgroundService: while (stoppingToken.IsCancellationRequested is false)");

            using var scope = serviceScopeFactory.CreateScope();

            var consumer = scope.ServiceProvider.GetRequiredService<IConsumer<string, string>>();

            var result = consumer.Consume(TimeSpan.FromSeconds(5));

            if (result is null) return;

            var outboxMessage = JsonSerializer.Deserialize<OutboxMessageEntity>(result.Message.Value)!;

            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxInboxDbContext>();

            var doesInboxMessageExist = await dbContext.Set<InboxMessageEntity>()
                .AnyAsync(im => im.Id == outboxMessage.Id, stoppingToken);

            if (doesInboxMessageExist) continue;


            dbContext.Set<InboxMessageEntity>().Add(
                new InboxMessageEntity
                {
                    Id = outboxMessage.Id,
                    Name = outboxMessage.Name,
                    Payload = outboxMessage.Payload,
                    ReceivedOn = DateTime.UtcNow,
                }
            );

            await dbContext.SaveChangesAsync(stoppingToken);

            consumer.Commit(result);
            consumer.Close();
            consumer.Dispose();
        }
    }

    public override void Dispose()
    {
        // consumer.Close();
        // consumer.Dispose();
        base.Dispose();
    }
}