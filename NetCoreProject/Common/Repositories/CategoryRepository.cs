using System.Collections.Generic;
using Common.EntityFramework;

namespace Common.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        //TODO: readonly?
        private NorthwindContext _context;

        public CategoryRepository(NorthwindContext context)
        {
            _context = context;
        }

        //TODO: make async
        public IEnumerable<Categories> GetAllCategories()
        {
            return _context.Categories;
        }
    }
}
