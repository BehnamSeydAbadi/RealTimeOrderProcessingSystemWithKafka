using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Infrastructure.OutboxMessages;
using OrderService.Infrastructure.Publishers;

namespace OrderService.Infrastructure;

public class OutboxBackgroundService(IServiceScopeFactory serviceScopeFactory, IProducer producer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxInboxDbContext>();

            var outboxMessages = await dbContext.Set<OutboxMessageEntity>()
                .Where(om => om.ProcessedOn == null).OrderBy(om => om.OccurredOn).Take(10).ToArrayAsync(stoppingToken);

            foreach (var outboxMessage in outboxMessages)
            {
                try
                {
                    await producer.ProduceAsync(outboxMessage.Payload, stoppingToken);
                    outboxMessage.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    outboxMessage.Error = ex.ToString();
                }
            }

            await dbContext.SaveChangesAsync(stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}