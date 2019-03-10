using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;
using Common.Services;

namespace NetCoreMentoring.Controllers
{
    //TODO: async all-the-way from controller to repository
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new CategoriesViewModel() {
                Categories = _categoryService.GetAllCategories()
            };

            return View(model);
        }            
    }
}
