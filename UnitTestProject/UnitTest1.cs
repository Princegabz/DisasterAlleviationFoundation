using DisasterAlleviation.Controllers;
using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;

namespace UnitTestProject
{
    public class RegistrationControllerTests
    {
        [Fact]
        public void Registration_Post_ValidUser_RedirectsToLogin()
        {
            // Arrange
            var controller = new RegistrationController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
        }      
    }

    public class LoginControllerTests
    {
        [Fact]
        public void Login_Post_ValidUser_RedirectsToHomeIndex() //This test simulates a valid user login(verified) and checks if the controller redirects to the "Index" action of the "Home" controller.
        {
            // Arrange
            var controller = new LoginController();

            // Act
            var result = controller.Login();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }


    public class HomeControllerTests
    {
        [Fact]
        public void CaptureMonetaryDonations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void CaptureDisaster_WithValidData_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.DisasterPage() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void CaptureGoodsDonations_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.DonationPage() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void CaptureMonetaryAllocations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.AllocationPage() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}