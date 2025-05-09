namespace OrderService.Infrastructure.Publishers;

public interface IProducer
{
    Task ProduceAsync(string payload, CancellationToken cancellationToken = default);
}