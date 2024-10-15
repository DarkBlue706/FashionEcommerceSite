using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;

namespace ShirtCompany.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserDBContext _userContext; // Injecting UserDBContext
    private readonly ProductDBContext _productContext; // Injecting ProductDBContext

    // Constructor to inject both UserDBContext and ProductDBContext
    public HomeController(ILogger<HomeController> logger, UserDBContext userContext, ProductDBContext productContext)
    {
        _logger = logger;
        _userContext = userContext; // Assign injected UserDBContext
        _productContext = productContext; // Assign injected ProductDBContext
    }

    // Other action methods can now access both contexts



    public IActionResult Index() //home/index abstract of all return types
    {
        return View();
    }

    public IActionResult Privacy() //home/privacy
    {
        return View();
    }
        public IActionResult Search() //home/privacy
    {
        return View();
    }

    public IActionResult Login(UserModel user)
    {
        if (ModelState.IsValid)
        {
            // Search for the user in the database by username or email
            var existingUser = _userContext.Users
                .FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            
            // Check if the user was found and the password is correct
            if (existingUser != null)
            {
                // Set authentication cookie, session, or whatever login mechanism you use
                // Example: HttpContext.Session.SetString("UserID", existingUser.UserID.ToString());
                
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        
        return View(user); // Return the view with the error messages if login fails
    }


        public IActionResult Register(UserModel user)
    {
    if (ModelState.IsValid)
    {
        _userContext.Users.Add(user);
        _userContext.SaveChanges();
        return RedirectToAction("Index");
    }
     return View(user);
    }
    public async Task<IActionResult> Shoes()
    {
        // Fetch all products where Category is "Shoes"
        var shoes = await _productContext.Product
            .Where(p => p.Category == "Shoes")
            .ToListAsync();

        // Pass the list of shoes to the view
        return View(shoes);
    }

        public async Task<IActionResult> Belts()
    {
        // Fetch all products where Category is "Shoes"
        var shoes = await _productContext.Product
            .Where(p => p.Category == "Belts")
            .ToListAsync();

        // Pass the list of shoes to the view
        return View(shoes);
    }

        public async Task<IActionResult> Glasses()
    {
        // Fetch all products where Category is "Shoes"
        var shoes = await _productContext.Product
            .Where(p => p.Category == "Glasses")
            .ToListAsync();

        // Pass the list of shoes to the view
        return View(shoes);
    }




    public async Task<IActionResult> ShowSearchForm()
    {
        return View();
    }
    
    // public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
    // {
    //     // Query the products in the database where the Category matches the SearchPhrase
    //     var filteredProducts = await _productContext.Product
    //         .Where(p => p.Category.Contains(SearchPhrase))
    //         .ToListAsync();
        
    //     // Return the filtered list of products to the view
    //     return View(filteredProducts);
    // }


    // If the model state is invalid, return the same view with the model to show validation errors
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}