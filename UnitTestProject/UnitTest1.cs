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
        public void Register_ReturnsViewResult()
        {
            // Arrange
            var controller = new RegistrationController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Registration_Post_ValidUser_RedirectsToLogin()
        {
            // Arrange
            var registerUserMock = new Mock<DisplayRecords>();
            registerUserMock.Setup(r => r.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new RegistrationController();

            // Act
            var result = controller.Registration(registerUserMock.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ActionName);
            Assert.Equal("Login", result.ControllerName);
        }

        [Fact]
        public void Registration_Post_InvalidUser_ReturnsViewResult()
        {
            // Arrange
            var registerUserMock = new Mock<DisplayRecords>();
            registerUserMock.Setup(r => r.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new RegistrationController();

            // Act
            var result = controller.Registration(registerUserMock.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Register", result.ViewName);
        }
    }
    public class LoginControllerTests
    {
        [Fact]
        public void Login_Get_ReturnsView() //This test checks if the Login action returns a ViewResult when it's accessed via a GET request.
        {
            // Arrange
            var controller = new LoginController();

            // Act
            var result = controller.Login();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Login_Post_ValidUser_RedirectsToHomeIndex() //This test simulates a valid user login(verified) and checks if the controller redirects to the "Index" action of the "Home" controller.
        {
            // Arrange
            var verifyServiceMock = new Mock<DisplayRecords>();
            verifyServiceMock.Setup(v => v.verified(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new LoginController();

            // Act
            var result = controller.Login(verifyServiceMock.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Login_Post_InvalidUser_ReturnsView() //This test simulates an invalid user login(not verified) and checks if the controller returns the "Login" view.
        {
            // Arrange
            var verifyServiceMock = new Mock<DisplayRecords>();
            verifyServiceMock.Setup(v => v.verified(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new LoginController();

            // Act
            var result = controller.Login(verifyServiceMock.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ViewName);
        }
    }



    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
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
        public void DonationPage_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.DonationPage() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void DisasterPage_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.DisasterPage() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CaptureMonetaryDonations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureMonetary(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureMonetaryDonations(mockDisplayRecords.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notification", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void CaptureMonetaryDonations_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureMonetary(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureMonetaryDonations(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("DonationPage", result.ViewName);
        }

        [Fact]
        public void CaptureDisaster_WithValidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureD(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureDisaster(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Home/Notification", result.ViewName);
        }

        [Fact]
        public void CaptureDisaster_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureD(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureDisaster(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("DonationPage", result.ViewName);
        }

        [Fact]
        public void CaptureGoodsDonations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureGoods(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureGoodsDonations(mockDisplayRecords.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notification", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void CaptureGoodsDonations_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureGoods(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureGoodsDonations(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("DonationPage", result.ViewName);
        }


        [Fact]
        public void CaptureMonetaryAllocations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureMonetaryAllocation(It.IsAny<int>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureMonetaryAllocations(mockDisplayRecords.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notification", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void CaptureMonetaryAllocations_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureMonetaryAllocation(It.IsAny<int>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureMonetaryAllocations(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AllocationPage", result.ViewName);
        }

        [Fact]
        public void CaptureGoodsAllocations_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureGoodsAllocation(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureGoodsAllocations(mockDisplayRecords.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notification", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void CaptureGoodsAllocations_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.CaptureGoodsAllocation(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.CaptureGoodsAllocations(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AllocationPage", result.ViewName);
        }

        [Fact]
        public void PurchaseGoods_WithValidData_ReturnsRedirectToAction()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.PurchaseGoods(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.PurchaseGoods(mockDisplayRecords.Object) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Notification", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void PurchaseGoods_WithInvalidData_ReturnsViewResult()
        {
            // Arrange
            var mockDisplayRecords = new Mock<DisplayRecords>();
            mockDisplayRecords.Setup(d => d.PurchaseGoods(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new HomeController(Mock.Of<ILogger<HomeController>>());

            // Act
            var result = controller.PurchaseGoods(mockDisplayRecords.Object) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("PurchaseGoods", result.ViewName);
        }
    }
}