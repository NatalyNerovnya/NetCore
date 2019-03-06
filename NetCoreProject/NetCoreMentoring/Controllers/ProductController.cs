using Common.Services;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;
using Product = Common.Models.Product;

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

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Method = "Add";
            var model = BuildAddEditProductModelByProductId(null);
            return View("AddEditView", model);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (product != null)
            {
                _productService.AddProduct(product);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int productId)
        {
            ViewBag.Method = "Edit";
            var model = BuildAddEditProductModelByProductId(productId);
            return View("AddEditView", model);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", product.Id);
            }

            if (product != null)
            {
                _productService.EditProduct(product);
            }

            return RedirectToAction("Index");
        }

        private AddEditProductViewModel BuildAddEditProductModelByProductId(int? productId)
        {
            return new AddEditProductViewModel()
            {
                Categories = _productService.GetCategories(),
                Suppliers = _productService.GetSuppliers(),
                Product = productId.HasValue ? _productService.GetProductById(productId.Value) : null
            };
        }
    }
}