using Microsoft.AspNetCore.Mvc;

namespace AnimalRefugeFinal.Controllers
{
    public class SecureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
