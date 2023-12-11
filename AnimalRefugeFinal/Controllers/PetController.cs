using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    }

}

