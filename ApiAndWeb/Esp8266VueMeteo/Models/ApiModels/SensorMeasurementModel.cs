using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class SensorMeasurementModel : MeasurementModel
    {
        public DateTimeOffset InsertDate { get; set; }
        public int? TemperatureF
        {
            get
            {
                if (Temperature.HasValue)
                {
                    return 32 + (int)(Temperature / 0.5556);
                } return null;
            }
        }
        public double? PressureHpa
        {
            get
            {
                if (Pressure.HasValue)
                {
                    var pressurehPa = (decimal)Pressure.Value / 100;
                    return (double?)Math.Round(pressurehPa, 0);
                } return null;
            }
        }
    }
}
