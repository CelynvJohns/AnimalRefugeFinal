using AnimalRefugeFinal.Controllers;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Threading.Tasks;

namespace AnimalRefugeFinalTest
{
    public class UnitTest1
    {
        private static Mock<UserManager<User>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(
                store.Object,
                Options.Create(new IdentityOptions()),
                new PasswordHasher<User>(),
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                new ServiceCollection().BuildServiceProvider(),
                null
            );

            userManagerMock.Setup(x => x.Users).Returns(new List<User>().AsQueryable());

            // Example setup for CreateAsync method
            userManagerMock
                .Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Adjusted setup for FindByIdAsync method
            userManagerMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns((string userId) => Task.FromResult<User>(null));

            return userManagerMock;
        }




        public async Task Register_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var userManagerMock = GetUserManagerMock();
            userManagerMock
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var model = new RegisterViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "testuser",
                Email = "test@example.com",
                Password = "Test123!32",
            };

            // Act
            var result = await controller.Register(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        // Add more test cases for different scenarios as needed


        [Fact]
        public async Task Register_InvalidModel_ReturnsView()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);
            controller.ModelState.AddModelError("key", "error");

            var model = new RegisterViewModel();

            // Act
            var result = await controller.Register(model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        public async Task Logout_LogoutSuccessful_RedirectsToHomeIndex()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            signInManagerMock.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await controller.Logout() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Login_Get_ReturnsView()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        public async Task Login_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            signInManagerMock.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);
            var model = new LoginViewModel { Username = "testuser", Password = "Test123!32" };

            // Act
            var result = await controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public async Task Login_InvalidModel_ReturnsView()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            signInManagerMock.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);
            controller.ModelState.AddModelError("key", "error");

            var model = new LoginViewModel();

            // Act
            var result = await controller.Login(model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        public void AccessDenied_ReturnsView()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = controller.AccessDenied() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        public void Index_ReturnsViewWithModel()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Session).Returns(sessionMock.Object);

            var controller = new FavoritesController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextMock.Object
                }
            };

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PetListViewModel>(result.Model);
        }

        public void Delete_ClearsFavoritesAndRedirectsToHomeIndex()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Session).Returns(sessionMock.Object);

            var controller = new FavoritesController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextMock.Object
                }
            };

            // Act
            var result = controller.Delete() as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.Null(result.RouteValues["Pets"]); // Make sure Pets route value is not set
        }


        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName); // Assumes default view name is used
        }

        [Fact]
        public void About_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName); // Assumes default view name is used
        }

        [Fact]
        public void Adoption_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Act
            var result = controller.Adoption() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName); // Assumes default view name is used
        }

        [Fact]
        public void Error_ReturnsViewResultWithErrorViewModel()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Act
            var result = controller.Error() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Error", result.ViewName);
            Assert.IsType<ErrorViewModel>(result.Model);
        }

        [Fact]
        public void NotFound_ReturnsViewResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Act
            var result = controller.NotFound() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.ViewName); // Assuming "NotFound" is the expected view name
        }

        [Fact]
        public void ErrorHandlingFilter_OnException_LogsErrorAndRedirectsToErrorView()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var filter = new HomeController.ErrorHandlingFilter(loggerMock.Object);
            var exceptionContext = new ExceptionContext(new ActionContext(), Array.Empty<IFilterMetadata>())
            {
                Exception = new Exception("Test exception")
            };

            // Act
            filter.OnException(exceptionContext);

            // Assert
            loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(exceptionContext.Result as ViewResult);
            Assert.Equal("Error", (exceptionContext.Result as ViewResult).ViewName);
        }


        [Fact]
        public void ViewList_ReturnsViewResultWithPets()
        {
            // Arrange
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Dog1", Species = "Dog" },
                new Pet { Id = 2, Name = "Cat1", Species = "Cat" }
            };

            var dbContextMock = new Mock<PetContext>();
            dbContextMock.Setup(c => c.Pets.ToList()).Returns(pets);

            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new PetController(dbContextMock.Object, userManagerMock.Object, httpContextAccessorMock.Object);

            // Act
            var result = controller.ViewList() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ViewList", result.ViewName);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Pet>>(result.Model);

            var modelPets = result.Model as List<Pet>;
            Assert.Equal(pets.Count, modelPets.Count);
        }


[Fact]
    public void Details_PetNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var dbContextMock = new Mock<PetContext>();
        var pets = new List<Pet>(); // Create an in-memory collection

        // Convert List<Pet> to DbSet<Pet>
        var petDbSet = new Mock<DbSet<Pet>>();
        petDbSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
        petDbSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
        petDbSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
        petDbSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(() => pets.AsQueryable().GetEnumerator());

        dbContextMock.Setup(c => c.Pets).Returns(petDbSet.Object);

        var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var controller = new PetController(dbContextMock.Object, userManagerMock.Object, httpContextAccessorMock.Object);

        // Act
        var result = controller.Details(1) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NotFound", result.ActionName);
    }





    [Fact]
        public void Anom_ReturnsViewResult()
        {
            // Arrange
            var controller = new SecureController();

            // Act
            var result = controller.Anom() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Anom", result.ViewName);
        }

        [Fact]
        public void Anom_AllowAnonymousAttributeApplied()
        {
            // Arrange
            var controllerType = typeof(SecureController);
            var anomMethodInfo = controllerType.GetMethod("Anom");

            // Act
            var allowAnonymousAttribute = anomMethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);

            // Assert
            Assert.NotNull(allowAnonymousAttribute);
            Assert.NotEmpty(allowAnonymousAttribute);
        }

        [Fact]
        public void ManagePet_ReturnsViewWithPets()
        {
            // Arrange
            var context = new Mock<PetContext>();
            var userManager = Mock.Of<UserManager<User>>();
            var controller = new AdminController(context.Object, userManager);

            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Pet1" },
                new Pet { Id = 2, Name = "Pet2" },
            };

            context.Setup(c => c.Pets).Returns(MockDbSet(pets));

            // Act
            var result = controller.ManagePet() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Pet>>(result.Model);
            Assert.Equal(2, (result.Model as IEnumerable<Pet>).Count());
        }

        [Fact]
        public void EditPet_WithValidPetId_ReturnsViewWithPet()
        {
            // Arrange
            var context = new Mock<PetContext>();
            var userManager = Mock.Of<UserManager<User>>();
            var controller = new AdminController(context.Object, userManager);

            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Pet1" },
                new Pet { Id = 2, Name = "Pet2" },
            };

            context.Setup(c => c.Pets.Find(1)).Returns(pets.FirstOrDefault(p => p.Id == 1));

            // Act
            var result = controller.EditPet(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Pet>(result.Model);
            Assert.Equal(1, (result.Model as Pet).Id);
        }

        [Fact]
        public void EditPet_WithInvalidPetId_ReturnsNotFound()
        {
            // Arrange
            var context = new Mock<PetContext>();
            var userManager = Mock.Of<UserManager<User>>();
            var controller = new AdminController(context.Object, userManager);

            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Pet1" },
                new Pet { Id = 2, Name = "Pet2" },
            };

            context.Setup(c => c.Pets.Find(99)).Returns((Pet)null);

            // Act
            var result = controller.EditPet(99) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        // Add more tests for other actions as needed

        // Helper method to mock DbSet
        private static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            return dbSet.Object;
        }

    }
}