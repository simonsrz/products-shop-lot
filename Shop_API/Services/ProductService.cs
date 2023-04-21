using Microsoft.EntityFrameworkCore;
using Shop_API.Data;
using Shop_API.Dto;
using Shop_API.Models;

namespace Shop_API.Services
{
    public class ProductService
    {

        private readonly ShopContext _shopDbContext;

        public ProductService(ShopContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _shopDbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _shopDbContext.Products.FirstOrDefault(x => x.id == id);
        }

        public Product AddProduct(ProductDto product)
        {
            Product newProduct = new Product(product.name, DateTime.Now, DateTime.Now, product.description, product.price);
            var result = _shopDbContext.Products.Add(newProduct);
            _shopDbContext.SaveChanges();
            return result.Entity;
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _shopDbContext.Products.Remove(product);
            _shopDbContext.SaveChanges();
        }

        public void UpdateProduct(int id, ProductDto product)
        {
            var productToUpdate = GetProductById(id);
            productToUpdate.price = product.price;
            productToUpdate.description = product.description;
            productToUpdate.editDate = DateTime.Now;
            productToUpdate.name = product.name;
            _shopDbContext.SaveChanges();
        }
    }
}
