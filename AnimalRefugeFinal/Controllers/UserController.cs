using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AnimalRefugeFinal.Models.AnimalRefugeFinal.Models;

namespace AnimalRefugeFinal.Controllers
{
    public class UserController : Controller
    {
        private readonly PetContext _context;

        public UserController(PetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

 
        // UserProfile "Profile" Action
        // Display the user's profile information
        // Allow users to edit and update their profiles
        public IActionResult Profile()
        {
            // Get the current user's ID
            var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            // Fetch the user's profile information
            var userProfile = _context.Users
                .Where(user => user.Id == userId)
                .FirstOrDefault();

            if (userProfile != null)
            {
                return View(userProfile);
            }

            // If the user profile is not found, you can handle it accordingly (e.g., redirect to an error page)
            return NotFound();
        }

        // EditProfile Action
        // Display a form for editing the user's profile
        // Handle POST request to update the user's profile
        [HttpGet]
        public IActionResult EditProfile()
        {
            
                // Get the current user's ID
                var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdString);

                // Fetch the user's profile information
                var userProfile = _context.Users
                    .Where(user => user.Id == userId)
                    .FirstOrDefault();

                if (userProfile != null)
                {
                    return View(userProfile);
                }

                // If the user profile is not found, you can handle it accordingly (e.g., redirect to an error page)
                return NotFound();
            

        }

        [HttpPost]
        public IActionResult EditProfile(User editedUser)
        {
            if (ModelState.IsValid)
            {
                // Get the current user's ID
                var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = int.Parse(userIdString);

                // Fetch the user's profile information
                var userProfile = _context.Users
                    .Where(user => user.Id == userId)
                    .FirstOrDefault();

                // Update the user's profile information
                userProfile.Username = editedUser.Username;
                userProfile.PasswordHash = editedUser.PasswordHash;
                userProfile.FirstName = editedUser.FirstName;
                userProfile.LastName = editedUser.LastName;


                _context.SaveChanges();

                // Redirect to the user's profile after successful profile update
                return RedirectToAction("Profile");
            }

            // If profile update fails, return to the edit profile page with errors
            return View(editedUser);
        }

        // Favorites Action
        // Display a list of pets that the user has marked as favorites
        public IActionResult Favorites()
        {
            // Add logic to fetch and display the user's favorite pets
            var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);
            var userFavorites = _context.Favorites
                .Include(f => f.Pet)
                .Where(f => f.UserId == userId)
                .Select(f => f.Pet)
                .ToList();
            return View(userFavorites);
        }

    }
}

