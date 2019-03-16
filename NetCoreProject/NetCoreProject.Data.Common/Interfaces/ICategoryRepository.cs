using NetCoreProject.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Data.Common.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<byte[]> GetImageByCategoryIdAsync(int categoryId);
        Task UpdateImageAsync(byte[] imageToUpload, int categoryId);
    }
}
