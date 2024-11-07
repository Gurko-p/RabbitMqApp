using System.Text;
using RabbitMQ.Client;

namespace MessageProducerApp
{
    internal class MessageProducer
    {
        public async Task ProduceAsync(string message)
        {
            var factory = new ConnectionFactory { HostName = "rabbitmq" };
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
            Console.WriteLine($" [x] Sent {message}");
        }
    }
}
