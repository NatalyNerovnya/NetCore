using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.Repositories;
using Microsoft.Extensions.Configuration;

namespace Common.Services
{
    public class ProductService : IProductService
    {
        private IConfiguration _configuration;
        private IProductRepository _productRepository;

        public ProductService(IConfiguration configuration, IProductRepository productRepository)
        {
            _configuration = configuration;
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetProducts()
        {
            var dtos = _productRepository.GetProducts();
            var productsNumber = _configuration.GetValue<int>("ProductsNumber");

            if (productsNumber == 0)
            {
                productsNumber = dtos.Count();
            }

            return dtos.Select(x => new Product()
            {
                Id = x.ProductId,
                CategoryName = x.Category.CategoryName,
                Discontinued = x.Discontinued,
                Name = x.ProductName,
                QuantityPerUnit = x.QuantityPerUnit,
                ReorderLevel = x.ReorderLevel,
                SupplierName = x.Supplier.CompanyName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                UnitsOnOrder = x.UnitsOnOrder
            }).Take(productsNumber);
        }
    }
}
