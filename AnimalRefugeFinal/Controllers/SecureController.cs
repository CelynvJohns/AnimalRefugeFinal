using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;

namespace AnimalRefugeFinal.Controllers
{
    public class SecureController : Controller
    {
        public SecureController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Anom()
        {
            try
            {
                // Your action logic here
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (customize based on your logging strategy)
                TempData["error"] = "An error occurred while processing the request.";
                return RedirectToAction("Index"); // Redirect to the appropriate action
            }
        }
    }
}
