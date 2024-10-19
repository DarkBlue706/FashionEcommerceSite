using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;


namespace ShirtCompany.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDBContext _userContext;

        // Constructor to inject UserDBContext
        public UsersController(UserDBContext userContext)
        {
            _userContext = userContext;
        }

        // GET: Users/Create - Display the form to create a new user
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create - Handle the form submission for a new user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Redirect to a relevant page after adding the user
            }
            return View(user); // If model validation fails, return the form with errors
        }

        // // Optionally: List all users (for testing or admin purposes)
        // public async Task<IActionResult> Index()
        // {
        //     var users = await _userContext.Users.ToListAsync();
        //     return View(users);
        // }
    }
}
