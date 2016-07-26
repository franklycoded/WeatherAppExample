using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.DataService.Configuration;

namespace WeatherForecast.DataService.External
{
    /// <summary>
    /// <see cref="IExternalApiService"/>
    /// </summary>
    public class ExternalApiService : IExternalApiService
    {
        private readonly IDataServiceConfiguration _dataServiceConfiguration;

        public ExternalApiService(IDataServiceConfiguration dataServiceConfiguration)
        {
            if (dataServiceConfiguration == null) throw new ArgumentNullException(nameof(dataServiceConfiguration));

            _dataServiceConfiguration = dataServiceConfiguration;
        }
        
        /// <summary>
        /// <see cref="IExternalApiService.GetForecast(string)"/>
        /// </summary>
        public async Task<ExternalForecastDto> GetForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentOutOfRangeException(nameof(city));

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(string.Format(_dataServiceConfiguration.UrlTemplate, city, _dataServiceConfiguration.ApiKey));

                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ExternalForecastDto>(jsonString);
                }

                throw new Exception(string.Format("Error (code: {0}) while getting forecast for {1}", result.StatusCode.ToString(), city));
            }
        }
    }
}
