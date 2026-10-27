using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Api.Models;
using WarehouseManager.Api.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WarehouseManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly WarehouseDbContext _context;

        public ProductsController(WarehouseDbContext context)
        {
            _context = context;
        }
      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    

        [HttpPost]
        public async  Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is PostgresException pgEx &&
                pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                pgEx.ConstraintName == "IX_Products_SKU"
            )
            {
                return Conflict("A product with this SKU already exists.");
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.SKU = updatedProduct.SKU;
            product.Description = updatedProduct.Description;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex) when (
                ex.InnerException is PostgresException pgEx &&
                pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                pgEx.ConstraintName == "IX_Products_SKU"
            )
            {
                return Conflict("A product with this SKU already exists.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}