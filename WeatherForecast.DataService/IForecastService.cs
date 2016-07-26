using System.Threading.Tasks;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService
{
    /// <summary>
    /// Service to request weather forecast for a specified city
    /// </summary>
    public interface IForecastService
    {
        /// <summary>
        /// Gets the forecast for the given city
        /// </summary>
        /// <param name="city">The city we are interested in</param>
        /// <returns>The multi-day forecast for the city</returns>
        Task<WeatherDto> GetForecastAsync(string city);
    }
}
