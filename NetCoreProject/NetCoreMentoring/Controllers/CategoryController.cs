using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;
using NetCoreProject.Domain.Interfaces;
using System.Threading.Tasks;

namespace NetCoreMentoring.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var model = new CategoriesViewModel() {
                Categories = categories
            };

            return View(model);
        }            
    }
}
