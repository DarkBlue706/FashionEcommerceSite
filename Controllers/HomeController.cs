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
        return RedirectToAction("SuccessfulRegister");
    }
    return View(user);
    }

    public IActionResult SuccessfulRegister()
    {
        return View();
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

    [HttpGet]
    public IActionResult Search(string query)
    {
        ViewData["SearchPhrase"] = query;

        if (string.IsNullOrWhiteSpace(query))
        {
            // Return all products if no query is provided
            var allProducts = _productContext.Product.ToList();
            return View(allProducts);
        }

        // Search for products where the category contains the query (case-insensitive)
        var result = _productContext.Product
            .Where(p => p.Category.Contains(query))
            .ToList();

        return View(result);
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
                // Query the database for products with a matching category
            var results = _productContext.Product
                .Where(p => p.Category.Contains(SearchPhrase))
                .ToList();

                // Return the results to a view
            return View(results);
        }
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}