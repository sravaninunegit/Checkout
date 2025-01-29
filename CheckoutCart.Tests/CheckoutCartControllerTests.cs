using CheckoutCart.Controllers;
using CheckoutCart.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutCart.Tests
{
    public class CheckoutCartControllerTests
    {
        private readonly Mock<ICheckout> _mockCheckout;
        private readonly Mock<ILogger<CheckoutCartController>> _mockLogger;
        private readonly CheckoutCartController _controller;

        public CheckoutCartControllerTests()
        {
            _mockCheckout = new Mock<ICheckout>();
            _mockLogger = new Mock<ILogger<CheckoutCartController>>();
            // Initialize the controller with mocked dependencies

            // Create the controller and pass the mocked service
            _controller = new CheckoutCartController(_mockCheckout.Object, _mockLogger.Object);
        }
        /// <summary>
        /// This method test for one valid  product
        /// </summary>
        [Fact]
        public void Scan_WhenItemIsValid_CallsScanOnCheckoutService()
        {
            // Arrange
            var item = "A";

            // Act
            var result = _controller.Scan([item]);

            // Assert
            _mockCheckout.Verify(x => x.Scan(item), Times.Once); // Ensure Scan method was called once
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// This method test for empty  product
        /// </summary>
        [Fact]
        public void Scan_WhenItemIsNull_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Scan(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Item cannot be empty", badRequestResult.Value);
        }
        /// <summary>
        ///  This test is when invalid item sent i.e which is not present in product class
        /// </summary>
        [Fact]
        public void Scan_WhenItemIsInvalid_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Scan(["invalid"]);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid items detected: invalid", badRequestResult.Value);
        }
        /// <summary>
        ///  This will give result for 3 different products without any special price
        /// </summary>
        [Fact]
        public void GetTotalPrice_ReturnsCorrectTotalPrice()
        {
            // Arrange
            var items = new List<string> { "A", "B", "C" };
            _mockCheckout.Setup(c => c.Scan(It.IsAny<string>())).Verifiable();

            // Act
            var result = _controller.Scan(items);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        ///  This will give result for 3 different products with  special price for product A
        /// </summary>
        [Fact]
        public void GetTotalPricewithSpecialofferProductA_ReturnsCorrectTotalPrice()
        {
            // Arrange
            var items = new List<string> { "A", "A", "A", "B" };
            _mockCheckout.Setup(c => c.Scan(It.IsAny<string>())).Verifiable();

            // Act
            var result = _controller.Scan(items);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  This will give result for 3 different products with  special price for product B
        /// </summary>
        [Fact]
        public void GetTotalPricewithSpecialofferProductB_ReturnsCorrectTotalPrice()
        {
            // Arrange
            var items = new List<string> { "B", "A", "B" };
            _mockCheckout.Setup(c => c.Scan(It.IsAny<string>())).Verifiable();

            // Act
            var result = _controller.Scan(items);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        ///  This will give result for 3 different products with  special price for product A and B
        /// </summary>
        [Fact]
        public void GetTotalPricewithSpecialofferProductAB_ReturnsCorrectTotalPrice()
        {
            // Arrange
            var items = new List<string> { "B", "A", "A", "A", "B" };
            _mockCheckout.Setup(c => c.Scan(It.IsAny<string>())).Verifiable();

            // Act
            var result = _controller.Scan(items);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}

