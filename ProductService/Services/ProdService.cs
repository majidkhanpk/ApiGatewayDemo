using ProductService.Model;
using ProductService.Repositories;

namespace ProductService.Services
{
    public class ProdService : IProdService
    {
        private readonly IProdRepository _productRepository;

        public ProdService(IProdRepository prodRepository)
        {
            _productRepository = prodRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product Create(Product product)
        {
            return _productRepository.Add(product);
        }
    }
}
