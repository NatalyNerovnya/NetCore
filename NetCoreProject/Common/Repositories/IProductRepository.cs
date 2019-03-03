using Common.EntityFramework;
using System.Collections.Generic;

namespace Common.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProducts();
    }
}
