using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DataService.Cache;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.DataContracts;
using WeatherForecast.DataService.External;

namespace WeatherForecast.DataService.Tests
{
    [TestClass]
    public class ForecastServiceTests
    {
        private Mock<IDataServiceConfiguration> _mockDataServiceConfiguration;
        private Mock<IExternalApiService> _mockExternalApiService;
        private Mock<IForecastCache> _mockForecastCache;

        [TestInitialize]
        public void Init()
        {
            _mockExternalApiService = new Mock<IExternalApiService>();
            _mockForecastCache = new Mock<IForecastCache>();
            _mockDataServiceConfiguration = new Mock<IDataServiceConfiguration>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ForecastService_NoConfigurationProvided_ArgumentNullException()
        {
            var forecastService = new ForecastService(_mockExternalApiService.Object, _mockForecastCache.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ForecastService_NoExternalApiServiceProvided_ArgumentNullException()
        {
            var forecastService = new ForecastService(null, _mockForecastCache.Object, _mockDataServiceConfiguration.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ForecastService_NoCacheProvided_ArgumentNullException()
        {
            var forecastService = new ForecastService(_mockExternalApiService.Object, null, _mockDataServiceConfiguration.Object);
        }

        [TestMethod]
        public void Test_GetForecast_ItemNotInCache_QueryDataService_AddToCache()
        {
            var weatherDto = new WeatherDto();
            var city = "myCity";
            var externalDto = new ExternalForecastDto()
            {
                city = new City()
                {
                    name = city
                },
                list = new List<List>()
            };

            _mockForecastCache.Setup(m => m.GetForecast(city)).Returns(default(WeatherDto));
            _mockExternalApiService.Setup(m => m.GetForecast(city)).ReturnsAsync(externalDto);

            var forecastService = new ForecastService(_mockExternalApiService.Object, _mockForecastCache.Object, _mockDataServiceConfiguration.Object);

            var result = forecastService.GetForecastAsync(city).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.City, city);
            _mockForecastCache.Verify(m => m.GetForecast(city), Times.Once);
            _mockForecastCache.Verify(m => m.AddForecast(city, It.IsAny<WeatherDto>()), Times.Once);
        }

        [TestMethod]
        public void Test_GetForecast_ItemInCache_ApiNotCalled_CachedItemReturned()
        {
            var city = "myCity";
            var weatherDto = new WeatherDto();
            weatherDto.City = city;

            _mockForecastCache.Setup(m => m.GetForecast(city)).Returns(weatherDto);

            var forecastService = new ForecastService(_mockExternalApiService.Object, _mockForecastCache.Object, _mockDataServiceConfiguration.Object);

            var result = forecastService.GetForecastAsync(city).Result;

            Assert.IsNotNull(result);
            Assert.AreSame(result, weatherDto);
            _mockExternalApiService.Verify(m => m.GetForecast(It.IsAny<string>()), Times.Never);
            _mockForecastCache.Verify(m => m.AddForecast(It.IsAny<string>(), It.IsAny<WeatherDto>()), Times.Never);
        }
    }
}
