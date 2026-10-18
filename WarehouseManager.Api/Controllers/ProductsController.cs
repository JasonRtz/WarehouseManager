using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Api.Models;

namespace WarehouseManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products = new()
        {
            new Product
            {
                Id = 1,
                Name = "Wireless Mouse",
                SKU = "WM-001",
                Description = "A high-precision wireless mouse.",
                Quantity = 150,
                Price = 29.99m
            },

            new Product
            {
                Id = 2,
                Name = "Mechanical Keyboard",
                SKU = "MK-002",
                Description = "A durable mechanical keyboard with customizable keys.",
                Quantity = 100,
                Price = 79.99m
            }
        };

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return products;
        }


        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                return product;
            }

            return NotFound();
        }
    

        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (products.Any(p => p.SKU == product.SKU))
            {
                return Conflict("A product with the same SKU already exists.");
            }
            
            if (products.Count > 0)
            {
                product.Id = products.Max(p => p.Id) + 1;
            }
            else
            {
                product.Id = 1; 
            }

            products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            if (products.Any(p => p.SKU == updatedProduct.SKU && p.Id != id))
            {
                return Conflict("A product with the same SKU already exists.");
            }

            product.Name = updatedProduct.Name;
            product.SKU = updatedProduct.SKU;
            product.Description = updatedProduct.Description;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);

            return NoContent();
        }

    }
}