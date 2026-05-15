using ProductService.Model;

namespace ProductService.Services
{
    public interface IProdService
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product Create(Product product);
    }
}
