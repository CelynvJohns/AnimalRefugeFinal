using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AnimalRefugeFinal.Controllers
{
    public class PetController : Controller
    {
        private readonly PetContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add this

        public PetController(PetContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor; // Initialize IHttpContextAccessor
        }

        public IActionResult Index()
        {
            return View();
        }

        // ViewList action
        // Display a list of all available pets with brief information on pets (name, age, species)
        public IActionResult ViewList()
        {
            var pets = _context.Pets.ToList();
            return View(pets);
        }

        // Details action
        // Display detailed information about a specific pet when a user clicks on a pet card
        // (name, species, age, description, special care instructions, photo)
        public IActionResult Details(int id)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // FilterByType Action
        // Filter by species
        public IActionResult FilterByType(string species)
        {
            // get spicies to list for filtering
            var filteredPets = _context.Pets.Where(p => p.Species == species).ToList();

            // Pass the filtered pets to the view
            return View("ViewList", filteredPets);
        }

        // FilterByAge Action
        // Filter by age range
        public IActionResult FilterByAge(string ageRange)
        {
            // Parse the selected age range
            var ageBounds = ageRange.Split('-').Select(int.Parse).ToArray();

            // Filter pets by age range
            var filteredPets = _context.Pets.Where(p => p.Age >= ageBounds[0] && p.Age <= ageBounds[1]).ToList();

            // Pass the filtered pets to the view
            return View("ViewList", filteredPets);
        }


        // Filter Action
        // Filter pets by species, age, or both
        public IActionResult Filter(string species, string ageRange)
        {
            IQueryable<Pet> filteredPets = _context.Pets;

            // Filter by species
            if (!string.IsNullOrEmpty(species))
            {
                filteredPets = filteredPets.Where(p => p.Species == species);
            }

            // Filter by age range
            if (!string.IsNullOrEmpty(ageRange))
            {
                // Parse the selected age range
                var ageBounds = ageRange.Split('-').Select(int.Parse).ToArray();

                // Filter pets by age range
                filteredPets = filteredPets.Where(p => p.Age >= ageBounds[0] && p.Age <= ageBounds[1]);
            }

            // Pass the filtered pets to the view
            return View("ViewList", filteredPets.ToList());
        }

        // Action to handle adding a pet to favorites
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(string name)
        {
            var userId = await GetCurrentUserIdAsync();

            // Find the pet in the database based on the provided name
            var pet = _context.Pets.FirstOrDefault(p => p.Name == name);

            if (pet == null)
            {
                return NotFound(); // Pet not found
            }

            // Check if the pet is already in the user's favorites
            var existingFavorite = _context.Favorites.FirstOrDefault(f => f.UserId == userId && f.PetId == pet.Id);

            if (existingFavorite != null)
            {
                // Pet is already in favorites
                return BadRequest("Pet is already in favorites.");
            }

            // Add the pet to the user's favorites
            var favorite = new Favorite
            {
                UserId = userId,
                PetId = pet.Id
            };

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            // Update the session to reflect the changes in user's favorites
            var petSession = new PetSession(_httpContextAccessor.HttpContext.Session);
            var userFavorites = petSession.GetUserFavorites();
            userFavorites.Add(pet);
            petSession.SetUserFavorites(userFavorites);

            return Ok(); // Success
        }

        private async Task<string> GetCurrentUserIdAsync()
        {
            // Get the username from the ClaimsPrincipal
            var username = User.Identity.Name;

            // If you're not inside a controller where User is available directly, you can use HttpContext
            // var username = HttpContext.User.Identity.Name;

            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);

            // Return the user's ID
            return user?.Id;
        }


    }

}

