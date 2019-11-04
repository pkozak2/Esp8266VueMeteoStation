using Newtonsoft.Json;
using System.Collections.Generic;

namespace Esp8266VueMeteo.Models
{
    public class SensorUpdateRequest
    {
        [JsonProperty(PropertyName = "esp8266id")]
        public string EspId { get; set; }
        [JsonProperty(PropertyName = "sensordatavalues")]
        public IList<SensorData> DataValues { get; set; }

        public SensorUpdateRequest()
        {
            DataValues = new List<SensorData>();
        }
    }

    public class SensorData
    {
        [JsonProperty(PropertyName = "value_type")]
        public string ValueType { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
