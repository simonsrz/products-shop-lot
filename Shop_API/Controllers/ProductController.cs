using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> getAllProducts()
        {
            return Ok(_shopDbContext.Products.ToList());
        }

        [HttpPost]
        public ActionResult<IEnumerable<ProductDto>> addProduct([FromBody] ProductDto product)
        {
            Product toAdd = new Product(product.id,product.name, product.creationDate, product.editDate, product.description, product.price );
            
            _shopDbContext.Products.Add(toAdd);
            _shopDbContext.SaveChanges();

            return Ok();
        }
    }
}
