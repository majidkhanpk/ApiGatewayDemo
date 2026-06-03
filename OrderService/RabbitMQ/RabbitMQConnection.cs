using RabbitMQ.Client;

namespace OrderService.RabbitMQ
{
    public class RabbitMQConnection
    {
        private readonly IConnection _connection;

        public RabbitMQConnection(IConfiguration config)
        {
            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMQ:Host"],
                Port = int.Parse(config["RabbitMQ:Port"]),
                UserName = config["RabbitMQ:UserName"],
                Password = config["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }
    }
}
