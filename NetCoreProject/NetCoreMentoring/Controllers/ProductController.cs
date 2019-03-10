using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.Models;
using NetCoreProject.Domain.Interfaces;
using System.Threading.Tasks;
using Product = NetCoreProject.Common.Product;

namespace NetCoreMentoring.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProductsViewModel()
            {
                Products = await _productService.GetProductsAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Method = "Add";
            var model = await BuildAddEditProductModelByProductIdAsync(null);
            return View("AddEditView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            if (product != null)
            {
                await _productService.AddProductAsync(product);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            ViewBag.Method = "Edit";
            var model = await BuildAddEditProductModelByProductIdAsync(productId);
            return View("AddEditView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", product.Id);
            }

            //TODO: why product can be null? in any case if product is null you will get null-reference exception in statement above
            if (product != null)
            {
                await _productService.EditProductAsync(product);
            }

            return RedirectToAction("Index");
        }

        private async Task<AddEditProductViewModel> BuildAddEditProductModelByProductIdAsync(int? productId)
        {
            return new AddEditProductViewModel()
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                Suppliers = await _supplierService.GetSuppliersAsync(),
                Product = productId.HasValue ? await _productService.GetProductByIdAsync(productId.Value) : null
            };
        }
    }
}