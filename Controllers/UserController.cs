using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShirtCompany.Models;
using Microsoft.AspNetCore.Authorization;


namespace ShirtCompany.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserDBContext _userContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserDBContext userContext, UserManager<ApplicationUser> userManager)
        {
            _userContext = userContext;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
        }

        // List all registered users (for admin purposes)
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
