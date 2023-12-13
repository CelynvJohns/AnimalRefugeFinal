using AnimalRefugeFinal.Controllers;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AnimalRefugeFinal.Tests.Controllers
{
    public class FavoritesControllerTests
    {
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

        [Fact]
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
    }
}
