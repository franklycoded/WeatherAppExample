using Newtonsoft.Json;
using System;
using System.Net.Http;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.ExternalDataContract;
using WeatherForecast.DataService.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecast.DataService
{
    /// <summary>
    /// <see cref="IForecastService"/>
    /// </summary>
    public class ForecastService : IForecastService
    {
        private readonly IDataServiceConfiguration dataServiceConfiguration;

        /// <summary>
        /// Creates a new instance of the ForecastService
        /// </summary>
        /// <param name="configuration">The data service configuration that specifies the external weather forecast api endpoint</param>
        public ForecastService(IDataServiceConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            dataServiceConfiguration = configuration;
        }

        /// <summary>
        /// <see cref="IForecastService.GetForecast(string)"/>
        /// </summary>
        public WeatherDto GetForecast(string city)
        {
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(string.Format(dataServiceConfiguration.UrlTemplate, city, dataServiceConfiguration.ApiKey)).Result;

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    var weatherData = JsonConvert.DeserializeObject<RootObject>(jsonString);

                    var weatherResult = new WeatherDto();
                    weatherResult.City = weatherData.city.name;
                    weatherResult.Country = weatherData.city.country;
                    weatherResult.DailyWeatherList = new List<DailyWeatherDto>();

                    foreach (var forecast in weatherData.list.Where(l => DateTime.Parse(l.dt_txt).Hour == 12).OrderBy(l => l.dt))
                    {
                        var date = DateTime.Parse(forecast.dt_txt);

                        weatherResult.DailyWeatherList.Add(new DailyWeatherDto()
                        {
                            Date = date,
                            DayName = date.DayOfWeek.ToString(),
                            IconPath = string.Format(dataServiceConfiguration.IconPathTemplate, forecast.weather[0].icon),
                            MiddayTemperature = forecast.main.temp_max,
                            ShortDescription = forecast.weather[0].description
                        });
                    }

                    return weatherResult;
                }

                return new WeatherDto();
            }
        }
    }
}
