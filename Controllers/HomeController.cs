using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace ShirtCompany.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserDBContext _userContext;
        private readonly ProductDBContext _productContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Inject necessary services
        public HomeController(
            ILogger<HomeController> logger, 
            UserDBContext userContext, 
            ProductDBContext productContext,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userContext = userContext;
            _productContext = productContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        // Login method using UserManager and SignInManager
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("UserID", user.Id);
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View();
        }

        // Registration method
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser user, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user);
        }

        public IActionResult SuccessfulRegister()
        {
            return View();
        }

        public async Task<IActionResult> Shoes()
        {
            var shoes = await _productContext.Product
                .Where(p => p.Category == "Shoes")
                .ToListAsync();
            return View(shoes);
        }

        public async Task<IActionResult> Belts()
        {
            var belts = await _productContext.Product
                .Where(p => p.Category == "Belts")
                .ToListAsync();
            return View(belts);
        }

        public async Task<IActionResult> Glasses()
        {
            var glasses = await _productContext.Product
                .Where(p => p.Category == "Glasses")
                .ToListAsync();
            return View(glasses);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search(string query)
        {
            ViewData["SearchPhrase"] = query;

            if (string.IsNullOrWhiteSpace(query))
            {
                var allProducts = _productContext.Product?.ToList() ?? new List<Product>();
                return View(allProducts);
            }

            // Use EF.Functions.Like for case-insensitive SQL filtering
            var result = _productContext.Product?
                .Where(p => EF.Functions.Like(p.Category ?? "", $"%{query}%"))
                .ToList();

            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            var results = _productContext.Product?
                .Where(p => (p.Category ?? "").Contains(SearchPhrase, StringComparison.OrdinalIgnoreCase))
                .ToList() ?? new List<Product>();

            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier ?? "Unknown";
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
