using System.Collections.Generic;
using Common.EntityFramework;

namespace Common.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private NorthwindContext _context;

        public CategoryRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            return _context.Categories;
        }
    }
}
