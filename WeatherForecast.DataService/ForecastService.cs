using System;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.DataContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.DataService.External;
using WeatherForecast.DataService.Cache;

namespace WeatherForecast.DataService
{
    /// <summary>
    /// <see cref="IForecastService"/>
    /// </summary>
    public class ForecastService : IForecastService
    {
        private readonly IExternalApiService _apiService;
        private readonly IForecastCache _cache;
        private readonly IDataServiceConfiguration _dataServiceConfiguration;

        /// <summary>
        /// Creates a new instance of the ForecastService
        /// </summary>
        /// <param name="apiService">The api service used to access the external forecast data</param>
        /// <param name="cache">Cache to improve performance when querying forecast data</param>
        /// <param name="dataServiceConfiguration">The configuration details for the data service</param>
        public ForecastService(IExternalApiService apiService, IForecastCache cache, IDataServiceConfiguration dataServiceConfiguration)
        {
            if (apiService == null) throw new ArgumentNullException(nameof(apiService));

            if (cache == null) throw new ArgumentNullException(nameof(cache));

            if (dataServiceConfiguration == null) throw new ArgumentNullException(nameof(dataServiceConfiguration));

            _apiService = apiService;
            _cache = cache;
            _dataServiceConfiguration = dataServiceConfiguration;
        }

        /// <summary>
        /// <see cref="IForecastService.GetForecastAsync(string)"/>
        /// </summary>
        public async Task<WeatherDto> GetForecastAsync(string city)
        {
            var forecast = _cache.GetForecast(city);

            if(forecast == null)
            {
                var rawForecastData = await _apiService.GetForecast(city);

                forecast = new WeatherDto();
                forecast.City = rawForecastData.city.name;
                forecast.Country = rawForecastData.city.country;
                forecast.DailyWeatherList = new List<DailyWeatherDto>();

                foreach (var rawForecast in rawForecastData.list.Where(l => DateTime.Parse(l.dt_txt).Hour == 12).OrderBy(l => l.dt))
                {
                    var date = DateTime.Parse(rawForecast.dt_txt);

                    forecast.DailyWeatherList.Add(new DailyWeatherDto()
                    {
                        Date = date,
                        DayName = date.DayOfWeek.ToString(),
                        IconPath = string.Format(_dataServiceConfiguration.IconPathTemplate, rawForecast.weather[0].icon),
                        MiddayTemperature = rawForecast.main.temp_max,
                        ShortDescription = rawForecast.weather[0].description
                    });
                }

                _cache.AddForecast(city, forecast);
            }

            return forecast;
        }
    }
}
