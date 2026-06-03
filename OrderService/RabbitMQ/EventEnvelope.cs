namespace OrderService.RabbitMQ
{
    public class EventEnvelope
    {
        public string EventType { get; set; }
        public int EventVersion { get; set; }
        public object Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
