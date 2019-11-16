using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Models
{
    public class MeasurementModel
    {
        public double? Pm10 { get; set; }
        public double? Pm25 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }
        public double? GpsTime { get; set; }
        public double? GpsDate { get; set; }
        public double? WifiRssi { get; set; }
        public double? CellVoltage { get; set; }

    }
}
