using DependencyInjection.Models;

namespace DependencyInjection.BL.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}
