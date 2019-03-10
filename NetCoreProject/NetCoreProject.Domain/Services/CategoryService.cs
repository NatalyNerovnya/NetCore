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
    }
}
