using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public IActionResult Favorites(string species = "All")
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var favorites = _context.Favorites
                .Include(f => f.Pet)
                .Where(f => f.UserId == userId)
                .Select(f => f.Pet)
                .ToList();

            // Filter favorites by species
            if (species != "All")
            {
                favorites = favorites.Where(p => p.Species == species).ToList();
            }

            // Initialize PetListViewModel
            var petListViewModel = new PetListViewModel
            {
                Pets = favorites,
            };

            // Set the 'Species' property after initializing 'petListViewModel'
            petListViewModel.Species = petListViewModel.GetDistinctSpecies();

            return View(petListViewModel);
        }




        // Add this method to your controller
        private List<string> GetDistinctSpecies(List<Pet> pets)
        {
            // Implement the logic to get distinct species from your Pets list
            // Assuming Pet has a property called Species

            var distinctSpecies = pets.Select(p => p.Species).Distinct().ToList();

            // Insert "All" at the beginning of the list
            distinctSpecies.Insert(0, "All");

            return distinctSpecies;
        }
    }
}
