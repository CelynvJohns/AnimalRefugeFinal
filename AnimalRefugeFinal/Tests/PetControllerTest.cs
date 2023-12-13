using AnimalRefugeFinal.Controllers;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AnimalRefugeFinal.Tests.Controllers
{
    public class PetControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var dbContextMock = new Mock<PetContext>();
            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new PetController(dbContextMock.Object, userManagerMock.Object, httpContextAccessorMock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);

            // Additional assertions as needed
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
            dbContextMock.Setup(c => c.Pets.FirstOrDefault(It.IsAny<Func<Pet, bool>>())).Returns((Pet)null);

            var userManagerMock = new Mock<UserManager<User>>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new PetController(dbContextMock.Object, userManagerMock.Object, httpContextAccessorMock.Object);

            // Act
            var result = controller.Details(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NotFound", result.ActionName);

        }
    }
}
