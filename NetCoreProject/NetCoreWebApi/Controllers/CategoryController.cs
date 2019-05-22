using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Common;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/categories
        [HttpGet]
        [Route("categories")]
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
        [Route("categories/{id}")]
        public async Task<ActionResult<byte[]>> GetImageBytesByIdAsync(int id)
        {
            var image = await _categoryService.GetImageByCategoryIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
        
        [HttpPost]
        [Route("categories/{id}")]
        public async Task<ActionResult> UpdateImageByIdAsync(int id, [FromBody] byte[] imageBytes)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateImageAsync(imageBytes, id);
            
            return Ok();
        }
    }
}