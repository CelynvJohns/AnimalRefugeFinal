using Microsoft.AspNetCore.Mvc;

namespace AnimalRefugeFinal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //ManagePet Action
        //get a list of all pets from the database
        //display table with pet info
        //- options to eidt, delete or mark pet as adopted


        //ManageUsers Action
        // - get list of all users from the database, include user info
        // - display table with user details
        // - option to view user profile, edit user information, or deactivate accounts


        //ManageApplications Action
        // - fetch a list of adoptioon applications submitted by users
        // - display application details - user name, app date, pet info
        // - provide options to approve or reject adoption applications
        // - status of applications - pending, approved, rejected
    }
}
