using AnimalRefugeFinal.Controllers;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AnimalRefugeFinal.Tests.Controllers
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Register_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            userManagerMock
                .Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var signInManagerMock = new Mock<SignInManager<User>>(userManagerMock.Object, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object);

            var model = new RegisterViewModel
            {
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

        // Add more tests as needed
    }
}
