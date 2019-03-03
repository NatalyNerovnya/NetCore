using Common.Services;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;

namespace NetCoreMentoring.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var model = new ProductsViewModel()
            {
                Products = _productService.GetProducts()
            };

            return View(model);
        }
    }
}