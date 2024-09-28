using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;

namespace ShirtCompany.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShirtsController: ControllerBase
    {

            //Need to validate products(checking for required fields, enforcing price ranges, etc)

            private List<Shirt> shirts = new List<Shirt>()
            {
                new Shirt { ShirtID = 1, Brand = "MyBrand", Color = "Blue", Gender = "women", Price = 30, Size = 10},
                new Shirt { ShirtID = 2, Brand = "MyBrand", Color = "Black", Gender = "Men", Price = 30, Size = 10},
                new Shirt { ShirtID = 3, Brand = "YourBrand", Color = "Pink", Gender = "men", Price = 30, Size = 10},
                new Shirt { ShirtID = 4, Brand = "YourBrand", Color = "Yellow", Gender = "Women", Price = 30, Size = 10},
            };

            [HttpGet]
            public string GetShirts() //Pull all products by creation date
            {
                    return "Reading all shirtss available";
            }

            [HttpGet("{id}")] //search for product by name or catergory
             public Shirt GetShirtById(int id)
            {
                return shirts.First(X=> X.ShirtID == id);
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