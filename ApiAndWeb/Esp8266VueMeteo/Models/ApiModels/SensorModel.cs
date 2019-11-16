using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class SensorModel
    {
        public Guid SensorId { get; set; }
        public string SensorName { get; set; }
        public string SensorDescription { get; set; }
        public string SensorDescritionExtra { get; set; }

    }
}
