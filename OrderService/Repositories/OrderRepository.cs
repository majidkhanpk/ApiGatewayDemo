using OrderService.Model;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new()
        {
            new Order { Id = 1, ProductName = "Laptop", Quantity = 1, TotalPrice = 1000 },
            new Order { Id = 2, ProductName = "Mouse", Quantity = 2, TotalPrice = 100 }
        };

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public Order Add(Order order)
        {
            order.Id = _orders.Max(o => o.Id) + 1;
            _orders.Add(order);
            return order;
        }
    }
}
