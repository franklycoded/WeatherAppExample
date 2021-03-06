﻿using System;
using System.Runtime.Serialization;

namespace WeatherForecast.DataService.DataContracts
{
    /// <summary>
    /// Dto that represents the weather each day
    /// </summary>
    [DataContract]
    public class DailyWeatherDto
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string DayName { get; set; }

        [DataMember]
        public string IconPath { get; set; }
        
        [DataMember]
        public double MiddayTemperature { get; set; }
        
        [DataMember]
        public string ShortDescription { get; set; }
    }
}
