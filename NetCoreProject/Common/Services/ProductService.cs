using System;
using System.Collections.Generic;
using System.Linq;
using Common.EntityFramework;
using Product = Common.Models.Product;
using Common.Repositories;
using Common.Settings;

namespace Common.Services
{
    public class ProductService : IProductService
    {
        private readonly ISettings _settings;
        private readonly IProductRepository _productRepository;

        public ProductService(ISettings settings, IProductRepository productRepository)
        {
            _settings = settings;
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetProducts()
        {
            var dtos = _productRepository.GetProducts().ToArray();
            var productsNumber = _settings.GetMaxProductsNumber();

            if (productsNumber <= 0)
            {
                productsNumber = dtos.Length;
            }

            return dtos.Select(BuildProduct).Take(productsNumber);
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void EditProduct(Product product)
        {
            _productRepository.EditProduct(product);
        }

        public IEnumerable<Categories> GetCategories()
        {
            return _productRepository.GetCategories();
        }

        public IEnumerable<Suppliers> GetSuppliers()
        {
            return _productRepository.GetSuppliers();
        }

        public Product GetProductById(int productId)
        {
            var productDto = _productRepository.GetProductById(productId);
            return productDto == null ? null : BuildProduct(productDto);
        }

        private Product BuildProduct(Products product)
        {
            return new Product()
            {
                Id = product.ProductId,
                CategoryName = product.Category.CategoryName,
                Discontinued = product.Discontinued,
                Name = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                SupplierName = product.Supplier.CompanyName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }
    }
}
