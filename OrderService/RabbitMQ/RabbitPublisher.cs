using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderService.RabbitMQ
{
    public class RabbitPublisher
    {
        private readonly IConfiguration _config;

        public RabbitPublisher(IConfiguration config)
        {
            _config = config;
        }

        public void Send(object message)
        {
            //var factory = new ConnectionFactory()
            //{
            //    HostName = _config["RabbitMQ:Host"]
            //};

            //using var connection = factory.CreateConnection();
            //using var channel = connection.CreateModel();

            //var queue = _config["RabbitMQ:Queue"];

            //channel.QueueDeclare(queue, true, false, false);

            //var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            //channel.BasicPublish("", queue, null, body);
        }
    }
}
