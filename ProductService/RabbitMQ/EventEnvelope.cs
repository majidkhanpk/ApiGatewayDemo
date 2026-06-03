using System.Text.Json;

namespace ProductService.RabbitMQ
{
    public class EventEnvelope
    {
        public string EventType { get; set; }
        public int EventVersion { get; set; }
        public JsonElement Data { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
