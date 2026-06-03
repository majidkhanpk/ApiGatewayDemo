using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace ProductService.RabbitMQ
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IConfiguration _config;
        private IConnection _connection;
        private IModel _channel;
        private string _queue;

        public OrderConsumer(RabbitMQConnection rabbitConnection, IConfiguration config)
        {
            _config = config;
            _connection = rabbitConnection.GetConnection();

            _channel = _connection.CreateModel();
            _queue = _config["RabbitMQ:Queue"];

            _channel.QueueDeclare(
                queue: _queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🔥 Consumer Started...");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("📩 RECEIVED MESSAGE 1");
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // 1. Deserialize envelope
                    var envelope = JsonSerializer.Deserialize<EventEnvelope>(message);

                    Console.WriteLine($"📩 Event Received: {envelope.EventType}");

                    // 2. Handle event type
                    if (envelope.EventType == "OrderCreated")
                    {
                        var order = JsonSerializer.Deserialize<OrderCreatedEvent>(envelope.Data.GetRawText());

                        Console.WriteLine($"🛒 OrderId: {order.Id}");
                        Console.WriteLine($"📦 Product: {order.ProductName}");
                        Console.WriteLine($"🔢 Quantity: {order.Quantity}");
                        Console.WriteLine($"🔢 Total Price: {order.TotalPrice}");

                        // 👉 BUSINESS LOGIC HERE
                        // Example: reduce stock
                        // _productService.ReduceStock(order.ProductId, order.Quantity);
                    }

                    // Manual ACK (safe processing)
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");

                    // Optional: requeue message
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(
                queue: _queue,
                autoAck: false,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }

    //public class OrderConsumer : BackgroundService
    //{
    //private readonly IConfiguration _config;

    //public OrderConsumer(IConfiguration config)
    //{
    //    _config = config;
    //}

    //protected override Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    var factory = new ConnectionFactory()
    //    {
    //        HostName = _config["RabbitMQ:Host"]
    //    };

    //    var connection = factory.CreateConnection();
    //    var channel = connection.CreateModel();

    //    var queue = _config["RabbitMQ:Queue"];

    //    channel.QueueDeclare(queue, true, false, false);

    //    var consumer = new EventingBasicConsumer(channel);

    //    consumer.Received += (model, ea) =>
    //    {
    //        var message = Encoding.UTF8.GetString(ea.Body.ToArray());

    //        Console.WriteLine($"📥 Received: {message}");
    //    };

    //    channel.BasicConsume(queue, true, consumer);

    //    return Task.CompletedTask;
    //}
    // }
}
