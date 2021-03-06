﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeatherForecast.DataService.DataContracts
{
    /// <summary>
    /// Dto that represents a multi-day weather forecast
    /// </summary>
    [DataContract]
    public class WeatherDto
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public List<DailyWeatherDto> DailyWeatherList { get; set; }
    }
}
