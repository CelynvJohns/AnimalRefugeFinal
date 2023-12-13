using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalRefugeFinal.Controllers
{
    public class AdminController : Controller
    {
        private readonly PetContext _context;

        public AdminController(PetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ManagePet Action
        // Get a list of all pets from the database
        // Display a table with pet info
        // Options to edit, delete, or mark pet as adopted
        public IActionResult ManagePet()
        {
            var pets = _context.Pets.ToList();
            return View(pets);
        }

        // EditPet Action
        // Display a form to edit pet information
        [HttpGet]
        public IActionResult EditPet(int petId)
        {
            var pet = _context.Pets.Find(petId);
            return View(pet);
        }

        // Handle POST request to process pet edits
        [HttpPost]
        public IActionResult EditPet(Pet editedPet)
        {
            if (ModelState.IsValid)
            {
                // Update the pet in the database
                _context.Pets.Update(editedPet);
                _context.SaveChanges();

                return RedirectToAction("ManagePet");
            }

            // If ModelState is not valid, return to the edit form with errors
            return View(editedPet);
        }

        // DeletePet Action
        // Delete a pet from the database
        public IActionResult DeletePet(int petId)
        {
            var pet = _context.Pets.Find(petId);

            if (pet != null)
            {
                // Remove the pet from the database
                _context.Pets.Remove(pet);
                _context.SaveChanges();
            }

            return RedirectToAction("ManagePet");
        }

        // MarkAsAdopted Action
        // Mark a pet as adopted
        public IActionResult MarkAsAdopted(int petId)
        {
            var pet = _context.Pets.Find(petId);

            if (pet != null)
            {
                //  mark the pet as adopted
                pet.IsAdopted = true;

                // Save changes to the database
                _context.SaveChanges();
            }

            return RedirectToAction("ManagePet");
        }

        
    

        // ManageUsers Action
        // Get a list of all users from the database, including user info
        // Display a table with user details
        // Option to view user profile, edit user information, or deactivate accounts
        public IActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // ViewUserProfile Action
        // Display the profile information of a specific user
        public IActionResult ViewUserProfile(String userId)
        {
            // Fetch the user's profile information
            var userProfile = _context.Users.Find(userId);

            if (userProfile != null)
            {
                return View(userProfile);
            }

            
            return NotFound();
        }


        public IActionResult EditUser(String userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                // Handle the case where the user is not found
                return NotFound();
            }

            // Display the user edit form
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                // Model validation failed, return to the edit user form with validation errors
                return View(updatedUser);
            }

            // Update the user's profile information
            var userProfile = _context.Users.Find(updatedUser.Id);

            if (userProfile != null)
            {
                userProfile.UserName = updatedUser.UserName;
                
                // Update other properties as needed

                // Save changes to the database
                _context.SaveChanges();
            }

            // Redirect to the user's profile page after editing
            return RedirectToAction("ManageUsers", new { userId = updatedUser.Id });
        }

        [HttpPost]
        public IActionResult DeleteUser(String userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                // Handle the case where the user is not found
                return NotFound();
            }

            // Remove the user from the database
            _context.Users.Remove(user);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the manage users page or any other desired page
            return RedirectToAction("ManageUsers");
        }



    }
}

