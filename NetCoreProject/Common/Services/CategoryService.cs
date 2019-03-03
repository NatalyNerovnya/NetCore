using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.Repositories;

namespace Common.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var dtos = _categoryRepository.GetAllCategories();

            return dtos.Select(x => new Category()
            {
                Id = x.CategoryId,
                Name = x.CategoryName
            });
        }
    }
}
