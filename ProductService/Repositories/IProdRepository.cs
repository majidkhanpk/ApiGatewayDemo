using ProductService.Model;

namespace ProductService.Repositories
{
    public interface IProdRepository
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product Add(Product product);
    }
}
