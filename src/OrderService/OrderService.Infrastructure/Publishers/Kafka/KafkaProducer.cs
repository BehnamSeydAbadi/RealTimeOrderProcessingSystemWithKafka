using Confluent.Kafka;

namespace OrderService.Infrastructure.Publishers.Kafka;

public class KafkaProducer(IProducer<Null, string> producer) : IProducer
{
    public async Task ProduceAsync(string payload, CancellationToken cancellationToken = default)
    {
        await producer.ProduceAsync(
            topic: "order-placed", new Message<Null, string> { Value = payload }, cancellationToken
        );
    }
}