using AnimalRefugeFinal.Models.AnimalRefugeFinal.Models;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace AnimalRefugeFinal.Controllers
{
    [Authorize] // Use this attribute if only authenticated users can apply for adoption
    public class AdoptionController : Controller
    {
        private readonly PetContext _context;

        public AdoptionController(PetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Apply(int petId)
        {
            // Assuming you have a view model for the adoption application form
            var viewModel = new AdoptionApplicationViewModel
            {
                PetId = petId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Apply(AdoptionApplicationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Model validation failed, return to the adoption application form with validation errors
                return View(viewModel);
            }

            // Get the current user's ID
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(userIdString);

            // Fetch the "Pending" status from the database
            var pendingStatus = _context.Statuses.FirstOrDefault(s => s.Name == "Pending");

            if (pendingStatus == null)
            {
                throw new InvalidOperationException("Pending status not found");
            }


            
            var application = new AdoptionApplication
            {
                UserId = userId,
                PetId = viewModel.PetId,
                Reasons = viewModel.Reasons,
                ApplicationDate = DateTime.Now,
                Status = pendingStatus// Set the initial status
            };

            // Add the adoption application to the database
            _context.AdoptionApplications.Add(application);
            _context.SaveChanges();

            // Redirect to a confirmation page or the pet details page
            return RedirectToAction("Details", "Pet", new { id = viewModel.PetId });
        }
    }
}

