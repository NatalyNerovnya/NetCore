using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Common;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // POST api/products
        [HttpPost]
        [Route("products")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if(product == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _productService.AddProductAsync(product);

            return Created(Request.Path, product);
        }

        // PUT api/products
        [HttpPut]
        [Route("products/{id}")]
        public async Task<ActionResult> Update(int id, Product product)
        { 
            if (product == null || !ModelState.IsValid || id != product.Id)
            {
                return BadRequest();
            }

            await _productService.EditProductAsync(product);
            return NoContent();
        }

        // DELETE api/products/1
        [HttpDelete]
        [Route("products/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return BadRequest();
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}