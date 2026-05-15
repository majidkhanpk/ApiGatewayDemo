using RabbitMQ.Client;

namespace ProductService.RabbitMQ
{
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
