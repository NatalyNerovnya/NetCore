using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreProject.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task<byte[]> GetImageByCategoryIdAsync(int categoryId)
        {
            return await _categoryRepository.GetImageByCategoryIdAsync(categoryId);
        }

        public async Task UpdateImageAsync(byte[] imageToUpload, int categoryId)
        {
            await _categoryRepository.UpdateImageAsync(imageToUpload, categoryId);
        }
    }
}
