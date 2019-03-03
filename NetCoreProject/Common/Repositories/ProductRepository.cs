using System.Collections.Generic;
using Common.EntityFramework;

namespace Common.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetProducts()
        {
            var products = _context.Products;

            return _context.Products;
        }
    }
}
