using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;
using ShirtCompany.Models.Repositories;
using ShirtCompany.Filters.ActionFilters;
using ShirtCompany.Filters.ExceptionFilters;


namespace ShirtCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShirtsController: ControllerBase
    {

            //Need to validate products(checking for required fields, enforcing price ranges, etc)


            [HttpGet]
            public IActionResult GetShirts() //Pull all shirts by creation date
            {
                    return Ok(ShirtRepository.GetShirts());
            }

            
            [HttpGet("{id}")] //search for product by name or catergory
            [Shirt_ValidatateShirtIdFilture]
            public IActionResult GetShirtById(int id) //return type for controller action methods ie return different types of responses
            {
                return Ok(ShirtRepository.GetShirtById(id));
            }

            [HttpPost]
            [Shirt_ValidateCreateShirtFilter]
             public IActionResult CreateShirt([FromBody]Shirt shirt) //Adding new product
            {
                ShirtRepository.AddShirt(shirt);
                return CreatedAtAction(nameof(GetShirtById),
                    new { id = shirt.ShirtID},
                    shirt);
            }
            
            [HttpPut("{id}")]
            [Shirt_ValidateUpdateShirtFilter]
            [Shirt_ValidatateShirtIdFilture]
            [Shirt_HandleUpdateExceptionsFilter]
             public IActionResult UpdateShirt(int id, Shirt shirt)
            {   
            
                ShirtRepository.UpdateShirt(shirt);
                
                return NoContent();
            }

            [HttpDelete("{id}")] //Delete by ID
             public string DeleteShirt(int id)
            {
                return $"Deleting shirt: {id}";
            }

    
    }
    
}


            // [HttpGet]
            // [Route("/products")]
            // public string GetProducts()
            // {
            //         return "Reading all products available";
            // }

            // [HttpGet]
            // [Route("/products/{id}")]
            //  public string GetProductById(int id)
            // {
            //     return $"Reading product: {id}";
            // }

            // [HttpPost]
            // [Route("/products")]
            //  public string CreateProduct()
            // {
            //     return $"Creating a shirt";
            // }
            
            // [HttpPut]
            // [Route("/products/{id}")]
            //  public string UpdateProduct(int id)
            // {
            //     return $"Updating product: {id}";
            // }

            // [HttpDelete]
            // [Route("/products/{id}")]
            //  public string DeleteProduct(int id)
            // {
            //     return $"Deleting product: {id}";
            // }