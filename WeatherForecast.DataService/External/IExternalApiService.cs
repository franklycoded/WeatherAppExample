using System.Threading.Tasks;

namespace WeatherForecast.DataService.External
{
    /// <summary>
    /// Queries the external forecast data service
    /// </summary>
    public interface IExternalApiService
    {
        /// <summary>
        /// Gets the weather forecast for the given city
        /// </summary>
        /// <param name="city">The city to get the forecast for</param>
        /// <returns>The forecast for the city</returns>
        Task<ExternalForecastDto> GetForecast(string city);
    }
}
