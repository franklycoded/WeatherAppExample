using Newtonsoft.Json;
using System;
using System.Net.Http;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.ExternalDataContract;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService
{
    public class ForecastService : IForecastService
    {
        private readonly IDataServiceConfiguration dataServiceConfiguration;

        public ForecastService(IDataServiceConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            dataServiceConfiguration = configuration;
        }

        public string GetData()
        {
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(string.Format(dataServiceConfiguration.UrlTemplate, "London", "UK", dataServiceConfiguration.ApiKey)).Result;

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    var weatherData = JsonConvert.DeserializeObject<RootObject>(jsonString);
                    return jsonString;
                }

                return "";
            }
        }

        public WeatherDto GetForecast(string city, string country)
        {
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetAsync(string.Format(dataServiceConfiguration.UrlTemplate, city, country, dataServiceConfiguration.ApiKey)).Result;

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    var weatherData = JsonConvert.DeserializeObject<RootObject>(jsonString);
                    return new WeatherDto(weatherData.list[0].weather[0].icon);
                }

                return new WeatherDto("nope");
            }
        }
    }
}
