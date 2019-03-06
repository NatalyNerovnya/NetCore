using Common.EntityFramework;
using System.Collections.Generic;
using Product = Common.Models.Product;

namespace Common.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
        void AddProduct(Product productDto);
        void EditProduct(Product product);
        IEnumerable<Categories> GetCategories();
        IEnumerable<Suppliers> GetSuppliers();
        Products GetProductById(int productId);
    }
}
