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

        // Registration Action
        // Display the user registration form
        // Handle POST request to register a new user
        // Validate user input and create a new user account
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                // Add logic to save the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to the login page after successful registration
                return RedirectToAction("Login");
            }

            // If registration fails, return to the registration page with errors
            return View(user);
        }

        // Login Action
        // Display the user login form
        // Handle POST request for user authentication
        // Redirect authenticated users to their profile
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Add logic for user authentication
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

            if (user != null)
            {
                // Redirect to the user's profile after successful login
                return RedirectToAction("Profile");
            }

            // If login fails, return to the login page with an error message
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        // Logout Action
        // Sign the user out and redirect to the homepage
        public IActionResult Logout()
        {
            

            return RedirectToAction("Index", "Home");
        }

        // UserProfile "Profile" Action
        // Display the user's profile information
        // Allow users to edit and update their profiles
        public IActionResult Profile()
        {
            // Add logic to fetch and display the user's profile information
            return View();
        }

        // EditProfile Action
        // Display a form for editing the user's profile
        // Handle POST request to update the user's profile
        [HttpGet]
        public IActionResult EditProfile()
        {
            // Add logic to fetch and display the user's profile information for editing
            return View();
        }

        [HttpPost]
        public IActionResult EditProfile(User editedUser)
        {
            if (ModelState.IsValid)
            {
                // Add logic to update the user's profile in the database
                var user = _context.Users.Find(editedUser.Id);
                user.Username = editedUser.Username;
                user.PasswordHash = editedUser.PasswordHash;
                user.FirstName = editedUser.FirstName;
                user.LastName = editedUser.LastName;
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

        // ApplyForAdoption Action
        // Allow the user to submit adoption applications for a specific pet
        // Display a form for adoption application
        // Handle POST request to process adoption applications
        [HttpGet]
        public IActionResult ApplyForAdoption(int petId)
        {
            // Retrieve the pet based on the provided petId
            var pet = _context.Pets.Find(petId);

            if (pet == null)
            {
                // Pet not found, you might want to handle this case (redirect to an error page or show a message)
                return NotFound();
            }

            // Create a new AdoptionApplicationViewModel to pass both the pet and adoption application form data to the view
            var viewModel = new AdoptionApplicationViewModel
            {
                PetId = pet.Id,
                PetName = pet.Name,
                // Add other necessary properties for your view model
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize] // Ensure that only authenticated users can submit adoption applications
        public IActionResult ApplyForAdoption(AdoptionApplicationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Model validation failed, return to the adoption application form with validation errors
                return View(viewModel);
            }

            // Create a new AdoptionApplication based on the submitted data
            var application = new AdoptionApplication
            {
                UserId = /* Get the current user's ID */
                PetId = viewModel.PetId,
                Reasons = viewModel.Reasons,
                ApplicationDate = DateTime.Now,
                Status = "Pending" // Set the initial status
            };

            // Add the adoption application to the database
            _context.AdoptionApplications.Add(application);
            _context.SaveChanges();

            //  redirect to a confirmation page or the pet details page
            return RedirectToAction("Details", "Pet", new { id = viewModel.PetId });
        }

        
    

    // TrackApplications Action
    // Display a list of the user's adoption applications and their statuses
       
        public IActionResult TrackApplications()
        {
            // Add logic to fetch and display the user's adoption applications
            return View();
        }

        // ScheduleVisit Action
        // Allow users to schedule visits to meet pets in person
        // Display a form for scheduling visits
        // Handle POST requests to process visit scheduling
        [HttpGet]
        public IActionResult ScheduleVisit(int petId)
        {
            // Add logic to fetch and display the visit scheduling form
            return View();
        }

        [HttpPost]
        public IActionResult ScheduleVisit(Visit visit)
        {
            if (ModelState.IsValid)
            {
                // Add logic to save the visit schedule to the database
                _context.Visits.Add(visit);
                _context.SaveChanges();

                // Redirect to a confirmation page or the user's profile
                return RedirectToAction("Profile");
            }

            // If visit scheduling fails, return to the scheduling form with errors
            return View(visit);
        }
    }
}

