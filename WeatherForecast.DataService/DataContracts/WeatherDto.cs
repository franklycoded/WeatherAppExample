using System.Runtime.Serialization;

namespace WeatherForecast.DataService.DataContracts
{
    [DataContract]
    public class WeatherDto
    {
        public WeatherDto(string icon)
        {
            this.Icon = icon;
        }

        [DataMember]
        public string Icon { get; private set; }
    }
}
