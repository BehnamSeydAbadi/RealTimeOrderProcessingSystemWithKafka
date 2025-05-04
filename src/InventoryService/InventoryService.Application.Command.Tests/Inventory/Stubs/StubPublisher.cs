using Mediator;

namespace InventoryService.Application.Command.Tests.Inventory.Stubs;

public class StubPublisher : IPublisher
{
    private StubPublisher()
    {
    }

    public static StubPublisher New() => new();

    public async ValueTask Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = new()) where TNotification : INotification
    {
        await ValueTask.CompletedTask;
    }

    public async ValueTask Publish(object notification, CancellationToken cancellationToken = new())
    {
        await ValueTask.CompletedTask;
    }
}