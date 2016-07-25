using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecast.DataService.Configuration;
using System.Configuration;

namespace WeatherForecast.DataService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var configuration =
                ConfigurationManager
                .GetSection("dataServiceConfiguration")
                as DataServiceConfiguration;

            var forecastService = new ForecastService(configuration);

            var result = forecastService.GetData();

        }
    }
}
