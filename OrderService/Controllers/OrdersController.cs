using Microsoft.AspNetCore.Mvc;
using OrderService.RabbitMQ;


namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[] { "Order1", "Order2" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Order {id}");
        }

        [HttpPost]
        public IActionResult Create([FromBody] object order)
        {
            return Ok("Order created");
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] object order,
                                 [FromServices] RabbitPublisher publisher)
        {
            publisher.Send(order);

            return Ok("Order sent to queue");
        }
    }
}
