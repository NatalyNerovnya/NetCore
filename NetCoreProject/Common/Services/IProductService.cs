using System.Collections.Generic;
using Common.EntityFramework;
using Product = Common.Models.Product;

namespace Common.Services
{
    public interface IProductService 
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);
        void EditProduct(Product product);
        IEnumerable<Categories> GetCategories();
        IEnumerable<Suppliers> GetSuppliers();
        Product GetProductById(int productId);
    }
}
