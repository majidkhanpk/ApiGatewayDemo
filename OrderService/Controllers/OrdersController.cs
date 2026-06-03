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
        private readonly RabbitPublisher _publisher;

        public OrdersController(IOrderMgrService orderMgrService, RabbitPublisher publisher)
        {
            _orderMgrService = orderMgrService;
            _publisher = publisher;
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
            // 1. Save in DB
            var createdOrder = _orderMgrService.Create(order);
            Console.WriteLine("new order is created"+ createdOrder.Id);
            // 2. Send event to RabbitMQ
            _publisher.SendOrderCreated(createdOrder);
            return Ok(createdOrder);
        }

        /*[HttpPost]
        public IActionResult CreateOrder([FromBody] Order order,
                                 [FromServices] RabbitPublisher publisher)
        {
            publisher.Send(order);

            return Ok("Order sent to queue");
        }*/
    }
}
