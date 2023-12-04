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

