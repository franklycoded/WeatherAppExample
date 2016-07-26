using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WeatherForecast.Controllers;
using WeatherForecast.DataService;

namespace WeatherForecast.Tests
{
    [TestClass]
    public class ForecastControllerTests
    {
        private Mock<IForecastService> _mockService;

        [TestInitialize]
        public void Init()
        {
            _mockService = new Mock<IForecastService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_NoForecastService_ArgumentNullException()
        {
            var controller = new ForecastController(null);
        }

        [TestMethod]
        public void Test_GetForecast_EmptyCity_Return400()
        {
            var controller = new ForecastController(_mockService.Object);

            var result = controller.GetForecast(" ").Result;

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Test_GetForecast_LongCityName_Return400()
        {
            var controller = new ForecastController(_mockService.Object);

            var result = controller.GetForecast("this is a very long city name. test test test test test test tests test test test test test test test test test test test ").Result;

            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Test_GetForecast_ForecastServiceFails_Return500()
        {
            _mockService.Setup(m => m.GetForecastAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            var controller = new ForecastController(_mockService.Object);

            var result = controller.GetForecast("test").Result;

            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }
    }
}
