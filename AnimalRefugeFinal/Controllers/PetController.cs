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

        // handles the POST request that's run when users click the "Add to Favorites" button on the ViewList page
        //this method will receive a ViewModel object as its parameter
        [HttpPost]
        public RedirectToActionResult Add(PetViewModel model)
        {
            model.Pet = _context.Pets.Where(t => t.Id == model.Pet.Id).FirstOrDefault();

            var session = new PetSession(HttpContext.Session);
            var pets = session.GetMyPets();
            pets.Add(model.Pet);
            session.SetPetList(pets);

            TempData["message"] = $"{model.Pet.Name} added to your favorites";

            return RedirectToAction("Index",
                new
                {
                    Pet = session.GetMyPets()
                }
                );
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

