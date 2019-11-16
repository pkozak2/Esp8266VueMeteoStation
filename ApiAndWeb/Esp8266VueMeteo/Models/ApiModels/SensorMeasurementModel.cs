using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class SensorMeasurementModel : MeasurementModel
    {
        public DateTimeOffset InsertDate { get; set; }
        public int TemperatureF
        {
            get
            {
                return 32 + (int)(Temperature / 0.5556);
            }
        }
    }
}
