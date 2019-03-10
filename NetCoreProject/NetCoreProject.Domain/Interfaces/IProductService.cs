using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task EditProductAsync(Product product);
    }
}
