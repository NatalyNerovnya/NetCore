using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreProject.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task EditProductAsync(Product product)
        {
            await _productRepository.EditProductAsync(product);
        }
    }
}
