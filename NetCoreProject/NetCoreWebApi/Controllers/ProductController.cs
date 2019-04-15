using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreProject.Common;
using NetCoreProject.Domain.Interfaces;

namespace NetCoreMentoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // POST api/product
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if(product == null)
            {
                return BadRequest();
            }

            await _productService.AddProductAsync(product);

            return Created(Request.Path, product);
        }

        // PUT api/product
        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        { 
            if (product == null)
            {
                return BadRequest();
            }

            await _productService.EditProductAsync(product);
            return NoContent();
        }

        // DELETE api/product
        [HttpDelete]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return BadRequest();
            }

            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}