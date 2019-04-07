using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;
using NetCoreProject.Domain.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreMentoring.Controllers
{
    public class CategoryController : Controller
    {
        private const string ImageContentType = "image/bmp";
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var model = new CategoriesViewModel()
            {
                Categories = categories
            };

            return View(model);
        }

        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _categoryService.GetImageByCategoryIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            var stream = new MemoryStream(image);
            return File(stream, ImageContentType);
        }

        [HttpGet]
        [ActionName("Update Image")]
        public async Task<IActionResult> UpdateImage(int categoryId)
        {
            var categoryViewModel = new UpdateCategoryViewModel()
            {
                Category = await _categoryService.GetCategoryByIdAsync(categoryId)
            };

            return View("UploadImageView", categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(IFormFile imageToUpload, int categoryId)
        {
            var stream = new MemoryStream();
            await imageToUpload.CopyToAsync(stream);
            await _categoryService.UpdateImageAsync(stream.ToArray(), categoryId);
            return RedirectToAction("Index");
        }
    }
}
