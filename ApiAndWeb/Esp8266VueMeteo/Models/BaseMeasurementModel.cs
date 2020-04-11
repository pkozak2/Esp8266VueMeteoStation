using Newtonsoft.Json;

namespace Esp8266VueMeteo.Models
{
    public class BaseMeasurementModel
    {
        public double? Pm10 { get; set; }
        public double? Pm25 { get; set; }
        public double? Co2 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        [JsonProperty(PropertyName = "heater_temperature")]
        public double? HeaterTemperature { get; set; }
        [JsonProperty(PropertyName = "heater_humidity")]
        public double? HeaterHumidity { get; set; }
    }
}
