using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.External;

namespace WeatherForecast.DataService.Tests.External
{
    [TestClass]
    public class ExternalApiServiceTests
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
            var externalApiService = new ExternalApiService(null);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Test_GetForecast_CityNull_ThrowAggregateException()
        {
            var externalApiService = new ExternalApiService(_mockDataServiceConfiguration.Object);
            var result = externalApiService.GetForecast(null).Result;
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Test_GetForecast_CityEmpty_ThrowAggregateException()
        {
            var externalApiService = new ExternalApiService(_mockDataServiceConfiguration.Object);
            var result = externalApiService.GetForecast(" ").Result;
        }
    }
}
