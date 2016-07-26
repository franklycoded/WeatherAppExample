using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService.Cache
{
    /// <summary>
    /// Cache for the weather forecasts
    /// </summary>
    public interface IForecastCache
    {
        /// <summary>
        /// Gets the forecast from the cache
        /// </summary>
        /// <param name="city">The city to get the forecast for</param>
        /// <returns>The forecast if it exists, null otherwise</returns>
        WeatherDto GetForecast(string city);

        /// <summary>
        /// Adds forecast to the cache
        /// </summary>
        /// <param name="city">The city to cache the forecast for</param>
        /// <param name="dto">The weather forecast for the city</param>
        void AddForecast(string city, WeatherDto dto);
    }
}
