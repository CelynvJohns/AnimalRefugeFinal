// FavoritesController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AnimalRefugeFinal.Models;  // Update with the correct namespace

[Authorize]  // Requires the user to be authenticated
public class FavoritesController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly PetContext _context;  // Assuming PetContext is your DbContext class

    public FavoritesController(UserManager<User> userManager, PetContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavorite(int petId)
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            // User not found or not authenticated
            return Json(new { success = false, message = "User not authenticated." });
        }

        // Check if the pet with the given ID exists
        var pet = _context.Pets.FirstOrDefault(p => p.Id == petId);

        if (pet == null)
        {
            return Json(new { success = false, message = "Pet not found." });
        }

        // Check if the pet is already in the user's favorites
        var isAlreadyFavorite = _context.Favorites.Any(f => f.UserId == user.Id && f.PetId == pet.Id);

        if (isAlreadyFavorite)
        {
            return Json(new { success = false, message = "Pet is already in favorites." });
        }

        // Add the pet to the user's favorites
        var favorite = new Favorite
        {
            UserId = user.Id,
            PetId = pet.Id
        };

        _context.Favorites.Add(favorite);
        _context.SaveChanges();

        return Json(new { success = true, message = "Added to favorites successfully." });
    }
}
