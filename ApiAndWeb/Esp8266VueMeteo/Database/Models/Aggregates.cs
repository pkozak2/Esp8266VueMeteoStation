using System;
using System.ComponentModel.DataAnnotations;

namespace Esp8266VueMeteo.Database.Models
{
    public class Aggregates
    {
        [Key]
        public Guid DeviceId { get; set; }
        [Key]
        public DateTimeOffset InsertDateTime { get; set; }
        [Key]
        public int Resolution { get; set; }
        public double? Pm25 { get; set; }
        public double? Pm10 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }
    }
}
