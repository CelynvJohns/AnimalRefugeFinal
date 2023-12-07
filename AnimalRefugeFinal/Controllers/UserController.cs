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
            // Get the current user's ID
            var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            // Fetch the user's adoption applications along with associated pet and status information
            var applications = _context.AdoptionApplications
                .Where(app => app.UserId == userId)
                .Include(app => app.Pets)
                .ToList();

            return View(applications);
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

