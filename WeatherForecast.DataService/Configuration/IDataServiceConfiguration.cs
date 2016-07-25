namespace WeatherForecast.DataService.Configuration
{
    /// <summary>
    /// Configuration information for querying the weather data service
    /// </summary>
    public interface IDataServiceConfiguration
    {
        /// <summary>
        /// Gets or sets the api key
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Gets or sets the url template
        /// </summary>
        string UrlTemplate { get; }
    }
}
