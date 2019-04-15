using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Data.EFModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Product = NetCoreProject.Common.Product;

namespace NetCoreProject.Data.Common.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(_mapper.Map<Products>(product));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditProductAsync(Product product)
        {
            _context.Products.Update(_mapper.Map<Products>(product));
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            return _mapper.Map<Product>(product);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<Product>>(products);
        }
    }
}
