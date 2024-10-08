using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;

namespace ShirtCompany.Controllers;

public class HomeController : Controller
{
        private readonly ILogger<HomeController> _logger;
        private readonly UserDBContext _context; // Injecting UserDBContext

        public HomeController(ILogger<HomeController> logger, UserDBContext context)
        {
            _logger = logger;
            _context = context; // Assign injected context
        }


    public IActionResult Index() //home/index abstract of all return types
    {
        return View();
    }

    public IActionResult Privacy() //home/privacy
    {
        return View();
    }

    public IActionResult AddUsers(UserModel user)
    {
    if (ModelState.IsValid)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // If the model state is invalid, return the same view with the model to show validation errors
    return View(user);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
