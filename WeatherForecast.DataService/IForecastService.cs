using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DataService.DataContracts;

namespace WeatherForecast.DataService
{
    public interface IForecastService
    {
        string GetData();

        WeatherDto GetForecast(string city, string country);
    }
}
