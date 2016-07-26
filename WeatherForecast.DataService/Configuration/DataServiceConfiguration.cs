using System;
using System.Configuration;

namespace WeatherForecast.DataService.Configuration
{
    /// <summary>
    /// <see cref="IDataServiceConfiguration"/>
    /// </summary>
    public class DataServiceConfiguration : ConfigurationSection, IDataServiceConfiguration
    {
        /// <summary>
        /// Reads the data service configuration from the config file
        /// </summary>
        /// <returns>The data service configuration</returns>
        public static DataServiceConfiguration GetConfiguration()
        {
            var configuration =
                ConfigurationManager
                .GetSection("dataServiceConfiguration")
                as DataServiceConfiguration;

            if (configuration != null) return configuration;

            throw new Exception("Can't find section 'dataServiceConfiguration'");
        }

        /// <summary>
        /// <see cref="IDataServiceConfiguration.ApiKey" />
        /// </summary>
        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get
            {
                return this["apiKey"].ToString();
            }
        }

        /// <summary>
        /// <see cref="IDataServiceConfiguration.UrlTemplate" />
        /// </summary>
        [ConfigurationProperty("urlTemplate", IsRequired = true)]
        public string UrlTemplate
        {
            get
            {
                return this["urlTemplate"].ToString();
            }
        }

        /// <summary>
        /// <see cref="IDataServiceConfiguration.IconPathTemplate" />
        /// </summary>
        [ConfigurationProperty("iconPathTemplate", IsRequired = true)]
        public string IconPathTemplate
        {
            get
            {
                return this["iconPathTemplate"].ToString();
            }
        }

        /// <summary>
        /// <see cref="IDataServiceConfiguration.CacheIntervalMinutes" />
        /// </summary>
        [ConfigurationProperty("cacheIntervalMinutes", IsRequired = true)]
        public int CacheIntervalMinutes
        {
            get
            {
                return Convert.ToInt32(this["cacheIntervalMinutes"]);
            }
        }
    }
}
