using AnimalRefugeFinal.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AnimalRefugeFinal.Tests.Controllers
{
    public class SecureControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new SecureController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
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

    }
}
