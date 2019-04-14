using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreWebApiClient
{
    public interface IClient
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Product>> GetProductsAsync();
    }
}
