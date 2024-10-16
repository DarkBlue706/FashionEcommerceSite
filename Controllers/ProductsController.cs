using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models; // Ensure this namespace includes your Product model and ProductDBContext


namespace ShirtCompany.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDBContext _productContext;

        // Constructor to inject ProductDBContext
        public ProductsController(ProductDBContext productContext)
        {
            _productContext = productContext;
        }

        public IActionResult Index() // Categories/Index
        {
            return View();
        }

        public async Task<IActionResult> Shirts()
        {
            // Fetch all products where the category is "Shirts"
            var shirts = await _productContext.Product
                .Where(p => p.Category == "Shirts")
                .ToListAsync();

            // Pass the list of shirts to the view
            return View(shirts);
        }

        public async Task<IActionResult> Pants()
        {
            // Fetch all products where the category is "Pants"
            var pants = await _productContext.Product
                .Where(p => p.Category == "Pants")
                .ToListAsync();

            return View(pants);
        }

        public async Task<IActionResult> Hats()
        {
            // Fetch all products where the category is "Hats"
            var hats = await _productContext.Product
                .Where(p => p.Category == "Hats")
                .ToListAsync();

            return View(hats);
        }

        public async Task<IActionResult> Shoes()
        {
            // Fetch all products where the category is "Shoes"
            var shoes = await _productContext.Product
                .Where(p => p.Category == "Shoes")
                .ToListAsync();

            return View(shoes);
        }
        public async Task<IActionResult> Belts()
        {
            // Fetch all products where the category is "Shoes"
            var shoes = await _productContext.Product
                .Where(p => p.Category == "Belts")
                .ToListAsync();

            return View(shoes);
        }
        public async Task<IActionResult> Glasses()
        {
            // Fetch all products where the category is "Shoes"
            var shoes = await _productContext.Product
                .Where(p => p.Category == "Glasses")
                .ToListAsync();

            return View(shoes);
        }


    }
}
