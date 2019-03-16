
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreProject.Common;
using NetCoreProject.Data.Common.Interfaces;
using NetCoreProject.Data.EFModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProject.Data.Common.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        private const int GarbageBytesNumber = 78;

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

        public async Task<byte[]> GetImageByCategoryIdAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            return FixImage(category?.Picture);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            return _mapper.Map<Category>(category);
        }

        public async Task UpdateImageAsync(byte[] imageToUpload, int categoryId)
        {
            var brokenImage = BreakImage(imageToUpload);
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if (category == null)
            {
                return;
            }
            category.Picture = brokenImage;
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        private byte[] BreakImage(byte[] image)
        {
            var additionalBytes = image.Take(GarbageBytesNumber);
            return additionalBytes.Concat(image).ToArray();
        }

        private byte[] FixImage(byte[] image)
        {
            return image?.Skip(GarbageBytesNumber).ToArray();
        }
    }
}
