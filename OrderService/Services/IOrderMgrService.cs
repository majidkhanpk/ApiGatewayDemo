using OrderService.Model;

namespace OrderService.Services
{
    public interface IOrderMgrService
    {
        List<Order> GetAll();
        Order GetById(int id);
        Order Create(Order order);
    }
}
