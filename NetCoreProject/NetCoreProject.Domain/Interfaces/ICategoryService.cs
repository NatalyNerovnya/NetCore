using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
