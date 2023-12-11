using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalRefugeFinal.Controllers
{
    public class UserController : Controller
    {
        private readonly PetContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(PetContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                return View(user);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User editedUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    user.UserName = editedUser.UserName;
                    user.Email = editedUser.Email;
                    user.FirstName = editedUser.FirstName;
                    user.LastName = editedUser.LastName;

                    await _userManager.UpdateAsync(user);

                    return RedirectToAction("Profile");
                }

                return NotFound();
            }

            return View(editedUser);
        }

        public IActionResult Favorites()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var userFavorites = _context.Favorites
                .Include(f => f.Pet)
                .Where(f => f.UserId == userId)
                .Select(f => f.Pet)
                .ToList();

            return View(userFavorites);
        }

        public IActionResult TrackApplications()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var applications = _context.AdoptionApplications
                .Include(a => a.Pets)
                .Where(a => a.UserId == userId)
                .ToList();

            return View(applications);
        }

    }
}
