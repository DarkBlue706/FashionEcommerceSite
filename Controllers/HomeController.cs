using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;

namespace ShirtCompany.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() //home/index abstract of all return types
    {
        return View();
    }

    public IActionResult Privacy() //home/privacy
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
