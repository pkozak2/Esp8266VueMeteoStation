using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Database.Models
{
    public class Records
    {
        [Key]
        public Guid DeviceId { get; set; }
        [Key]
        public DateTimeOffset InsertDateTime { get; set; }
        public double? Pm25 { get; set; }
        public double? Pm10 { get; set; }
        public double? Co2 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }
    }
}
