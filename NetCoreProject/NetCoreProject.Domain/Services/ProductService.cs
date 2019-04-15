using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreProject.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISettings _settings;

        public ProductService(IProductRepository productRepository, ISettings settings)
        {
            _productRepository = productRepository;
            _settings = settings;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();

            var maxProductNumber = _settings.GetMaximumProductNumber();
            if (maxProductNumber <= 0 )
            {
                maxProductNumber = products.Count();
            }

            return products.Take(maxProductNumber);
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task EditProductAsync(Product product)
        {
            await _productRepository.EditProductAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }
    }
}
