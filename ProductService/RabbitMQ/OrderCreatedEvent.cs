namespace ProductService.RabbitMQ
{
    public class OrderCreatedEvent
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
