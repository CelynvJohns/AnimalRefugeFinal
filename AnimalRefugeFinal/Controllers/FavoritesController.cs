// FavoritesController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AnimalRefugeFinal.Models;  // Update with the correct namespace

[Authorize]  // Requires the user to be authenticated
public class FavoritesController : Controller
{
    //calls Index() action method when user clicks on the "My Favorite Pets" link
    [HttpGet]
    public IActionResult Index()
    {
        // create new  PetSession object and posst to the Session property of the HttpContext property
        var session = new PetSession(HttpContext.Session);
        // create new PetListViewModel object to load it with data from tsession state 
        var model = new PetListViewModel
        {
            Pets = session.GetMyPets()
        };
        return View(model);
    }

    //called when user clicks "Clear Favorites" from Favorites page
    [HttpPost]
    public RedirectToActionResult Delete()
    {
        //create new PetSession object and passing it the Session property of the controller's HttpContext property
        var session = new PetSession(HttpContext.Session);
        //call RemoveMyPets() method of the CountrySession object
        session.RemoveMyPets();

        //store message in TempData to tell user the favorite country cleared, the layout displays this message
        TempData["message"] = "Favorite pets cleared";

        //redirect back to Home page, get Id values of active cat and game that are stored in session state and build the route parameters of the URL
        return RedirectToAction("Index", "Home",
            new
            {
                Pets = session.GetMyPets()
            });
    }
}
