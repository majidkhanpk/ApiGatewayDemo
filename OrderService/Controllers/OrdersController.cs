using Microsoft.AspNetCore.Mvc;
using OrderService.Model;
using OrderService.RabbitMQ;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderMgrService _orderMgrService;

        public OrdersController(IOrderMgrService orderMgrService)
        {
            _orderMgrService = orderMgrService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderMgrService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderMgrService.GetById(id);
            if ( order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            return Ok(_orderMgrService.Create(order));
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order,
                                 [FromServices] RabbitPublisher publisher)
        {
            publisher.Send(order);

            return Ok("Order sent to queue");
        }
    }
}
