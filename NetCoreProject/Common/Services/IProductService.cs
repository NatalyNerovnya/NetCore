using Common.Models;
using System.Collections.Generic;

namespace Common.Services
{
    public interface IProductService 
    {
        IEnumerable<Product> GetProducts();
    }
}
