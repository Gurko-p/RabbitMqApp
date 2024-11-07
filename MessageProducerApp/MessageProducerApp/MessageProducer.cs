using System.Text;
using RabbitMQ.Client;

namespace MessageProducerApp
{
    internal class MessageProducer
    {
        public async Task ProduceAsync()
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

            var i = 500;
            while (i < int.MaxValue)
            {
                i++;
                Thread.Sleep(3000);
                var message = $"Hello message №{i}";
                var body = Encoding.UTF8.GetBytes(message);
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
                Console.WriteLine($" [x] Sent {message}");
            }
        }
    }
}
