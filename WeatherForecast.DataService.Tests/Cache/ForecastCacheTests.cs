using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WeatherForecast.DataService.Cache;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService.Tests.Cache
{
    [TestClass]
    public class ForecastCacheTests
    {
        private Mock<IDataServiceConfiguration> _mockDataServiceConfiguration;

        [TestInitialize]
        public void Init()
        {
            _mockDataServiceConfiguration = new Mock<IDataServiceConfiguration>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_NoDataServiceConfiguration_ThrowArgumentNullException()
        {
            var externalApiService = new ForecastCache(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_AddForecast_EmptyCity_ThrowArgumentOutOfRangeException()
        {
            var cache = new ForecastCache(_mockDataServiceConfiguration.Object);
            cache.AddForecast(" ", new WeatherDto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddForecast_NullWeatherDto_ThrowArgumentNullException()
        {
            var cache = new ForecastCache(_mockDataServiceConfiguration.Object);
            cache.AddForecast("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_GetForecast_EmptyCity_ThrowArgumentOutOfRangeException()
        {
            var cache = new ForecastCache(_mockDataServiceConfiguration.Object);
            var result = cache.GetForecast(" ");
;       }

        [TestMethod]
        public void Test_CacheEmpty_GetForecastReturnsNull()
        {
            _mockDataServiceConfiguration.SetupGet(d => d.CacheIntervalMinutes).Returns(60);

            var cache = new ForecastCache(_mockDataServiceConfiguration.Object);

            var result = cache.GetForecast("mycity");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_CacheNotEmpty_GetForecastReturnsForecast()
        {
            _mockDataServiceConfiguration.SetupGet(d => d.CacheIntervalMinutes).Returns(60);

            var cityName = "mycity";
            var dto = new WeatherDto()
            {
                City = cityName
            };

            var cache = new ForecastCache(_mockDataServiceConfiguration.Object);

            cache.AddForecast(cityName, dto);

            var result = cache.GetForecast(cityName);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.City, cityName);
            Assert.AreSame(result, dto);
        }
    }
}
