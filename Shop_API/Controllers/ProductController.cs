using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_API.Data;
using Shop_API.Dto;
using Shop_API.Models;
using Shop_API.Response;
using Shop_API.Services;

namespace Shop_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductResponse>> getAllProducts()
        {
            return Ok(_productService.GetProductList());
        }

        [HttpGet("{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductResponse> getProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProductResponse>> addProduct([FromBody] ProductDto product)
        {
            Product productToAdd = _productService.AddProduct(product);

            return CreatedAtAction(nameof(getProductById), new { id = productToAdd.id }, productToAdd);
        }

        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductResponse>> deleteProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return Ok(product);
        }

        [HttpPut("{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductResponse>> updateProduct(int id, [FromBody] ProductDto product)
        {
            var productToUpdate = _productService.GetProductById(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }
            _productService.UpdateProduct(id, product);

            return Ok(productToUpdate);
        }
    }
}
