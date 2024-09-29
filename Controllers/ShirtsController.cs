using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;
using ShirtCompany.Models.Repositories;

namespace ShirtCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShirtsController: ControllerBase
    {

            //Need to validate products(checking for required fields, enforcing price ranges, etc)


            [HttpGet]
            public string GetShirts() //Pull all products by creation date
            {
                    return "Reading all shirtss available";
            }

            [HttpGet("{id}")] //search for product by name or catergory
             public IActionResult GetShirtById(int id) //returns multiple results
            {
                if (id <= 0) // 0 or less
                    return BadRequest();

                var shirt = ShirtRepository.GetShirtById(id);
                if(shirt == null)
                    return NotFound();
                return Ok(shirt);
            }

            [HttpPost]
             public string CreateShirt([FromBody]Shirt shirt) //Adding new product
            {
                return $"Creating a shirt";
            }
            
            [HttpPut("{id}")]
             public string UpdateShirt(int id)
            {
                return $"Updating shirt: {id}";
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