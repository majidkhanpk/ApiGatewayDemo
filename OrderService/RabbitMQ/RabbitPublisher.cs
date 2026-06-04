using RabbitMQ.Client;
using System.Collections;
using System.Data.Common;
using System.Text;
using System.Text.Json;

namespace OrderService.RabbitMQ
{
    public class RabbitPublisher
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queue;

        public RabbitPublisher(RabbitMQConnection rabbitConnection, IConfiguration config)
        {
            _config = config;
            _connection = rabbitConnection.GetConnection();
            _channel = _connection.CreateModel();
            _queue = _config["RabbitMQ:Queue"];

            // Declare queue once
            /*_channel.QueueDeclare(
                queue: _queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );*/

            // Declare exchange
            _channel.ExchangeDeclare(
                exchange: "order.exchange",
                type: ExchangeType.Topic,
                durable: true
            );

        }

        public void SendOrderCreated(object order)
        {
            var envelope = new EventEnvelope
            {
                EventType = "OrderCreated",
                EventVersion = 1,
                Data = order,
                Timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(envelope);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            //Using queue name as routing key for simplicity
            /*_channel.BasicPublish(
                exchange: "",
                routingKey: _queue,
                basicProperties: properties,
                body: body
            );*/

            _channel.BasicPublish(
                exchange: "order.exchange",
                routingKey: "order.created",
                basicProperties: properties,
                body: body
            );

            Console.WriteLine("📤 Sent: order.created");
        }
    }
}
