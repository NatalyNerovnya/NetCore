using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Common;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreMentoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet]
        [Route("image")]
        public async Task<ActionResult<byte[]>> GetImageBytesByIdAsync(int categoryId)
        {
            var image = await _categoryService.GetImageByCategoryIdAsync(categoryId);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
        
        [HttpPost]
        [Route("image")]
        public async Task<ActionResult> UpdateImageByIdAsync(int categoryId, [FromBody] byte[] imageBytes)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateImageAsync(imageBytes, categoryId);
            
            return Ok();
        }
    }
}