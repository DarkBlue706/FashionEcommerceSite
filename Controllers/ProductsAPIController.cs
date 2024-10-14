using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;
using ShirtCompany.Filters.ActionFilters;
using ShirtCompany.Filters.ExceptionFilters;

namespace ShirtCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsAPIController : ControllerBase
    {
        private readonly ProductDBContext _context; // Injected DbContext

        public ProductsAPIController(ProductDBContext context)
        {
            _context = context; // Assign injected context
        }

        // GET: api/shirts
        // Pull all shirts ordered by creation date
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _context.Product
                    .OrderByDescending(s => s.CreatedDate)
                    .ToListAsync(); // Fetch all shirts from the database
                return Ok(products); // Return 200 OK with the list of shirts
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/shirts/{id}
        // Fetch a shirt by ID
        [HttpGet("{id}")]
        [ServiceFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if the shirt is not found
                }
                return Ok(product); // Return 200 OK with the shirt
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/shirts
        // Create a new shirt
        [HttpPost]
        [Product_ValidateCreateProductFilter]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Product.Add(product); // Add the new shirt to the context
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, product); // Return 201 Created
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            return BadRequest(ModelState); // Return 400 Bad Request if model validation fails
        }

        // PUT: api/shirts/{id}
        // Update an existing shirt
        [HttpPut("{id}")]
        [Product_ValidateUpdateProductFilter]
        [ServiceFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        [ServiceFilter(typeof(Product_HandleUpdateExceptionsFilterAttribute))] 
        public async Task<IActionResult> UpdateShirt(int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _context.Product.FindAsync(id);
                    if (existingProduct == null)
                    {
                        return NotFound(); // Return 404 if the shirt is not found
                    }

                    // Update properties
                    existingProduct.Name = product.Name;
                    existingProduct.Brand = product.Brand;
                    existingProduct.Description = product.Description;
                    existingProduct.Color = product.Color;
                    existingProduct.Size = product.Size;
                    existingProduct.Price = product.Price;
                    existingProduct.Gender = product.Gender;
                    existingProduct.Category = product.Category;
                    existingProduct.UpdatedDate = DateTime.Now; // Update the modified date

                    _context.Product.Update(existingProduct); // Update the shirt in the context
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return NoContent(); // Return 204 No Content
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            return BadRequest(ModelState); // Return 400 Bad Request if model validation fails
        }

        // DELETE: api/shirts/{id}
        // Delete a shirt by ID
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if the shirt is not found
                }

                _context.Product.Remove(product); // Remove the shirt from the context
                await _context.SaveChangesAsync(); // Save changes to the database

                return Ok($"Product with ID {id} deleted successfully"); // Return success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
