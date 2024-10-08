using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CatergoriesController : Controller
    {

        public IActionResult Index() //Catergories/Index
        {
            return View();
        }

        public IActionResult Edit (int? id) //combine model and view
        {
            var catergory = new Catergory { CatergoryId = id.HasValue?id.Value:0}; //instance of model

            return View(catergory);
        }

        public IActionResult Shirts()
        {
            // Logic for Category 1 page
            return View();
        }

        public IActionResult Pants()
        {
            // Logic for Category 2 page
            return View();
        }

        public IActionResult Hats()
        {
            // Logic for Category 3 page
            return View();
        }

        
    }


}