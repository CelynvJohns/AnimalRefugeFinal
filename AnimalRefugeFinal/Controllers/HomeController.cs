using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimalRefugeFinal.Controllers
{
    public class HomeController : Controller
    {
        //diplay home page
        //
        public IActionResult Index()
        {
            return View();
        }

        //display about page
        public IActionResult About()
        {
            return View();
        }

    }
}