using Microsoft.AspNetCore.Mvc;

namespace AnimalRefugeFinal.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Registration Action
        //Display the user registration form
        //Handle POST request to register a new user
        //Validate user inpute and create a new user account


        //Login Action
        //Display the user login form
        //Handle POST request for user authentication
        //Redirect authnticated users to their profile


        //Logout Action
        //Sign the user out and redirect to the homepage

        //UserProfile "Profile" Action
        //Display the user's prfile information
        //Allow users to edit and update their profiles


        //Favorites Action
        //Display a list of pets that the user has marked as favorites

        //ApplyForAdoption Action
        //Allow user to submit adoption applications for a specific pet
        //Display a form for adoption application
        //Handle POST request to process adoption applications


        //TrackApplications Action
        //Display a list of the user's adoption applications and their statuses

        //ScheduleVisit Action
        //Allow users to schedule visits to meet pets in person
        //Display a form for scheduling visits
        //Handle POST requests to process visit scheduling

    }
}
