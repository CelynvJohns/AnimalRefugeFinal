using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalRefugeFinal.Controllers
{
    public class AdoptionApplicationController : Controller
    {
        public IActionResult AdoptionApplication()
        {
            // Create an instance of AdoptionApplication model
            var model = new AnimalRefugeFinal.Models.AdoptionApplication();

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Set the username in the model
                model.User = new User { Username = User.Identity.Name };
            }

            return View(model);
        }
    }
}
