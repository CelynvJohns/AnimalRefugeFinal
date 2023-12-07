using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

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
            return View();
        }
    }
}
