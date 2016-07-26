using System;
using System.Runtime.Caching;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService.Cache
{
    /// <summary>
    /// <see cref="IForecastCache"/>
    /// </summary>
    public class ForecastCache : IForecastCache
    {
        private readonly MemoryCache _cache;
        private readonly IDataServiceConfiguration _dataServiceConfiguration;

        public ForecastCache(IDataServiceConfiguration dataServiceConfiguration)
        {
            if (dataServiceConfiguration == null) throw new ArgumentNullException(nameof(dataServiceConfiguration));

            _cache = new MemoryCache("forecastCache");
            _dataServiceConfiguration = dataServiceConfiguration;
        }

        /// <summary>
        /// <see cref="IForecastCache.AddForecast(string, WeatherDto)"/>
        /// </summary>
        public void AddForecast(string city, WeatherDto dto)
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentOutOfRangeException(nameof(city));
            if (dto == null) throw new ArgumentNullException();

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_dataServiceConfiguration.CacheIntervalMinutes);

            _cache.Add(city, dto, policy);
        }

        /// <summary>
        /// <see cref="IForecastCache.GetForecast(string)"/>
        /// </summary>
        public WeatherDto GetForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentOutOfRangeException(nameof(city));

            var item = _cache.Get(city);

            if (item != null) return (WeatherDto)item;

            return null;
        }
    }
}
