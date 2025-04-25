using Mediator;

namespace OrderService.Application.Command.Tests.Order.Stubs;

public class StubPublisher : IPublisher
{
    private StubPublisher()
    {
    }

    public static StubPublisher New() => new();

    public async ValueTask Publish<TNotification>(TNotification notification,
        CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
    {
        await ValueTask.CompletedTask;
    }

    public async ValueTask Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
    {
        await ValueTask.CompletedTask;
    }
}