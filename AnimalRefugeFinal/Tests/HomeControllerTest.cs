using AnimalRefugeFinal.Controllers;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace AnimalRefugeFinal.Tests.Controllers
{
    public class HomeControllerTests
    {
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
    }
}
