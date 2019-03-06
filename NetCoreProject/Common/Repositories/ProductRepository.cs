using System.Collections.Generic;
using System.Linq;
using Common.EntityFramework;
using Product = Common.Models.Product;

namespace Common.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetProducts()
        {
            return _context.Products;
        }

        public void AddProduct(Product product)
        {
            var productDto = BuildProductDto(product);
            _context.Add(productDto);
            _context.SaveChanges();
        }

        public void EditProduct(Product product)
        {
            var productDto = BuildProductDto(product);
            productDto.ProductId = product.Id;

            _context.Update(productDto);
            _context.SaveChanges();
        }

        public IEnumerable<Categories> GetCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Suppliers> GetSuppliers()
        {
            return _context.Suppliers;
        }

        public Products GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(x => x.ProductId == productId);
        }

        private Products BuildProductDto(Product product)
        {
            return new Products()
            {
                ProductName = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Discontinued = product.Discontinued,
                ReorderLevel = product.ReorderLevel,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }
    }
}
