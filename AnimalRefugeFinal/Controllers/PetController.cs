using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using System.Linq;

namespace AnimalRefugeFinal.Controllers
{
    public class PetController : Controller
    {
        private readonly PetContext _context;

        public PetController(PetContext context)
        {
            _context = context;
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
            // Assuming there's a Species property in the Pet model
            var filteredPets = _context.Pets.Where(p => p.Species == species).ToList();

            // Pass the filtered pets to the view
            return View("ViewList", filteredPets);
        }
    }
}

