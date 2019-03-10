
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Data.EFModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreProject.Data.Common.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<Category>>(categories);
        }
    }
}
