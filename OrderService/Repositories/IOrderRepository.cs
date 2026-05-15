using OrderService.Model;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Add(Order order);
    }
}
