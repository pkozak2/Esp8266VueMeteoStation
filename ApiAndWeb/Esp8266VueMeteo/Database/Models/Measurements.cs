using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Database.Models
{
    public class Measurements
    {
        public Measurements()
        {
            MeasurementId = Guid.NewGuid();
            InsertDateTime = DateTimeOffset.Now;
        }
        [Key]
        public Guid MeasurementId { get; set; }
        [Required]
        public Guid DeviceId { get; set; }
        [Required]
        public DateTimeOffset InsertDateTime { get; set; }
        public double? Pm25 { get; set; }
        public double? Pm10 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }

        public Devices Device { get; set; }
    }
}
