using System;
using System.Configuration;

namespace WeatherForecast.DataService.Configuration
{
    /// <summary>
    /// <see cref="IDataServiceConfiguration"/>
    /// </summary>
    public class DataServiceConfiguration : ConfigurationSection, IDataServiceConfiguration
    {
        public static DataServiceConfiguration GetConfiguration()
        {
            var configuration =
                ConfigurationManager
                .GetSection("dataServiceConfiguration")
                as DataServiceConfiguration;

            if (configuration != null) return configuration;

            throw new Exception("Can't find section 'dataServiceConfiguration'");
        }

        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get
            {
                return this["apiKey"].ToString();
            }
        }

        [ConfigurationProperty("urlTemplate", IsRequired = true)]
        public string UrlTemplate
        {
            get
            {
                return this["urlTemplate"].ToString();
            }
        }
    }
}
