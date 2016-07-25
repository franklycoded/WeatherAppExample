namespace WeatherForecast.DataService.Configuration
{
    /// <summary>
    /// Configuration information for querying the weather data service
    /// </summary>
    public interface IDataServiceConfiguration
    {
        /// <summary>
        /// Gets the api key
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Gets the url template
        /// </summary>
        string UrlTemplate { get; }

        /// <summary>
        /// Gets the icon path template
        /// </summary>
        string IconPathTemplate { get; }
    }
}
