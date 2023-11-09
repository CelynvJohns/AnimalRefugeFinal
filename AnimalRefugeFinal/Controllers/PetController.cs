using Microsoft.AspNetCore.Mvc;

namespace AnimalRefugeFinal.Controllers
{
    public class PetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //ViewList action
        //Display a list of all available perts with brief information on pets(name, age, species)


        //Details action
        //display detailed information about a specific pet when a user licks on pet card(name, species, age description, and special care instructions,photo)



        //FilterByType Action
        //Filter by species



    }
}
