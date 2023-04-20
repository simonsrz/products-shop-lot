using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_API.Data;
using Shop_API.Dto;
using Shop_API.Models;

namespace Shop_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _shopDbContext;

        public ProductController(ShopContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductDto>> getAllProducts()
        {
            return Ok(_shopDbContext.Products.ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> getProductById(int id)
        {
            var product = _shopDbContext.Products.FirstOrDefault(p => p.id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProductDto>> addProduct([FromBody] ProductDto product)
        {
            Product productToAdd = new Product(product.id,product.name, product.creationDate, product.creationDate, product.description, product.price );
            
            _shopDbContext.Products.Add(productToAdd);
            _shopDbContext.SaveChanges();

            return CreatedAtAction(nameof(getProductById), new { id = productToAdd.id }, productToAdd);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductDto>> deleteProduct(int id)
        {
            var product = _shopDbContext.Products.FirstOrDefault(p => p.id == id);
            if (product == null)
            {
                return NotFound();
            }

            _shopDbContext.Products.Remove(product);
            _shopDbContext.SaveChanges();

            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductDto>> updateProduct(int id, [FromBody] ProductDto product)
        {
            var productToUpdate = _shopDbContext.Products.FirstOrDefault(p => p.id == id);

            if (productToUpdate == null)
            {
                return NotFound();
            }
            productToUpdate.price = product.price;
            productToUpdate.description = product.description;
            productToUpdate.editDate = DateTime.Now;
            productToUpdate.name = product.name;
            _shopDbContext.SaveChanges();

            return Ok(productToUpdate);
        }
    }
}
