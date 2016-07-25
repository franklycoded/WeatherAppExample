using System;
using System.Web.Http;
using WeatherForecast.DataService;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.Controllers
{
    [RoutePrefix("api/forecast")]
    public class ForecastController : ApiController
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            if (forecastService == null) throw new ArgumentNullException(nameof(forecastService));

            _forecastService = forecastService;
        }

        [HttpGet]
        [Route("{country}/{city}")]
        public WeatherDto GetForecast(string country, string city)
        {
            return _forecastService.GetForecast(city, country);
        }
    }
}
