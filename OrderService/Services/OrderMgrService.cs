using OrderService.Model;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderMgrService :IOrderMgrService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderMgrService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Order Create(Order order)
        {
            // future place for business logic (discount, validation, etc.)
            return _orderRepository.Add(order);
        }
    }
}
