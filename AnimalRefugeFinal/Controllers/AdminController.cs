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
                // Implement logic to mark the pet as adopted
                
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

        // ManageApplications Action
        // Fetch a list of adoption applications submitted by users
        // Display application details - user name, app date, pet info
        // Provide options to approve or reject adoption applications
        // Status of applications - pending, approved, rejected
        public IActionResult ManageApplications()
        {
            var applications = _context.AdoptionApplications
                .Include(a => a.User)
                .Include(a => a.Pets)
                .ToList();

            return View(applications);
        }
    }
}

