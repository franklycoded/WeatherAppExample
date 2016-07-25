using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DataService.Configuration;

namespace WeatherForecast.DataService.Tests
{
    [TestClass]
    public class ForecastServiceTests
    {
        private Mock<IDataServiceConfiguration> mockDataServiceConfiguration;

        [TestInitialize]
        public void Init()
        {
            mockDataServiceConfiguration = new Mock<IDataServiceConfiguration>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_ForecastService_NoConfigurationProvided_ArgumentNullException()
        {
            var forecastService = new ForecastService(null);
        }
    }
}
