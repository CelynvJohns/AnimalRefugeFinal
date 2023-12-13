using Microsoft.AspNetCore.Mvc;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using System.Security.Claims;
using Xunit;

namespace AnimalRefugeFinal.Controllers
{
    public class UserController : Controller
    {
        private readonly PetContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(PetContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    return View(user);
                }

                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Redirect to an error view or display a generic error message
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    return View(user);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Redirect to an error view or display a generic error message
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User editedUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    if (user != null)
                    {
                        user.UserName = editedUser.UserName;
                        user.Email = editedUser.Email;

                        await _userManager.UpdateAsync(user);

                        return RedirectToAction("Profile");
                    }

                    return NotFound();
                }

                return View(editedUser);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Redirect to an error view or display a generic error message
                return RedirectToAction("Error");
            }
        }

        public IActionResult Favorites(string species = "All")
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var favorites = _context.Favorites
                    .Include(f => f.Pet)
                    .Where(f => f.UserId == userId)
                    .Select(f => f.Pet)
                    .ToList();

                // Filter favorites by species
                if (species != "All")
                {
                    favorites = favorites.Where(p => p.Species == species).ToList();
                }

                // Initialize PetListViewModel
                var petListViewModel = new PetListViewModel
                {
                    Pets = favorites,
                };

                // Set the 'Species' property after initializing 'petListViewModel'
                petListViewModel.Species = GetDistinctSpecies(favorites);

                return View(petListViewModel);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // Redirect to an error view or display a generic error message
                return RedirectToAction("Error");
            }
        }

        // Add this method to your controller
        private List<string> GetDistinctSpecies(List<Pet> pets)
        {
            // Implement the logic to get distinct species from your Pets list
            // Assuming Pet has a property called Species

            var distinctSpecies = pets.Select(p => p.Species).Distinct().ToList();

            // Insert "All" at the beginning of the list
            distinctSpecies.Insert(0, "All");

            return distinctSpecies;
        }

        // Error action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Log the error if needed
            // Display a custom error view with the error details
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Fact]
        public async Task Profile_ReturnsViewWithUser()
        {
            // Arrange
            var context = new Mock<PetContext>();
            var userManager = MockUserManager();
            var controller = new UserController(context.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "TestUser"),
                        new Claim(ClaimTypes.NameIdentifier, "TestId"),
                    }, "mock"))
                }
            };

            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new User { Id = "TestId", UserName = "TestUser" });

            // Act
            var result = await controller.Profile() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal("TestUser", (result.Model as User).UserName);
        }

        [Fact]
        public async Task Profile_WithNullUser_RedirectsToLogin()
        {
            // Arrange
            var context = new Mock<PetContext>();
            var userManager = MockUserManager();
            var controller = new UserController(context.Object, userManager.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };

            userManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

            // Act
            var result = await controller.Profile() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ActionName);
            Assert.Equal("User", result.ControllerName);
        }

        // Add more tests for other actions as needed

        // Helper method to mock UserManager
        private static Mock<UserManager<User>> MockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

    }
}
