﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherForecast.DataService;

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
        [Route("{city}")]
        public async Task<IHttpActionResult> GetForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city)) return BadRequest("The City parameter cannot be empty!");
            if (city.Length > 50) return BadRequest("The City parameter cannot be longer than 50 characters!");

            try
            {
                return Ok(await _forecastService.GetForecastAsync(city));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
