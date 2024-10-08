using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;
using ShirtCompany.Filters.ActionFilters;
using ShirtCompany.Filters.ExceptionFilters;

namespace ShirtCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private readonly ShirtDBContext _context; // Injected DbContext

        public ShirtsController(ShirtDBContext context)
        {
            _context = context; // Assign injected context
        }

        // GET: api/shirts
        // Pull all shirts ordered by creation date
        [HttpGet]
        public async Task<IActionResult> GetShirts()
        {
            try
            {
                var shirts = await _context.Shirt
                    .OrderByDescending(s => s.CreatedDate)
                    .ToListAsync(); // Fetch all shirts from the database
                return Ok(shirts); // Return 200 OK with the list of shirts
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/shirts/{id}
        // Fetch a shirt by ID
        [HttpGet("{id}")]
        [Shirt_ValidatateProductIdFilture]
        public async Task<IActionResult> GetShirtById(int id)
        {
            try
            {
                var shirt = await _context.Shirt.FindAsync(id);
                if (shirt == null)
                {
                    return NotFound(); // Return 404 if the shirt is not found
                }
                return Ok(shirt); // Return 200 OK with the shirt
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/shirts
        // Create a new shirt
        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public async Task<IActionResult> CreateShirt([FromBody] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Shirt.Add(shirt); // Add the new shirt to the context
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ProductID }, shirt); // Return 201 Created
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
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_ValidatateProductIdFilture]
        [Shirt_HandleUpdateExceptionsFilter]
        public async Task<IActionResult> UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingShirt = await _context.Shirt.FindAsync(id);
                    if (existingShirt == null)
                    {
                        return NotFound(); // Return 404 if the shirt is not found
                    }

                    // Update properties
                    existingShirt.Name = shirt.Name;
                    existingShirt.Brand = shirt.Brand;
                    existingShirt.Description = shirt.Description;
                    existingShirt.Color = shirt.Color;
                    existingShirt.Size = shirt.Size;
                    existingShirt.Price = shirt.Price;
                    existingShirt.Gender = shirt.Gender;
                    existingShirt.Category = shirt.Category;
                    existingShirt.UpdatedDate = DateTime.Now; // Update the modified date

                    _context.Shirt.Update(existingShirt); // Update the shirt in the context
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
        [Shirt_ValidatateProductIdFilture]
        public async Task<IActionResult> DeleteShirt(int id)
        {
            try
            {
                var shirt = await _context.Shirt.FindAsync(id);
                if (shirt == null)
                {
                    return NotFound(); // Return 404 if the shirt is not found
                }

                _context.Shirt.Remove(shirt); // Remove the shirt from the context
                await _context.SaveChangesAsync(); // Save changes to the database

                return Ok($"Shirt with ID {id} deleted successfully"); // Return success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
