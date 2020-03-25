using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
        public double? Co2 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }
        public double? WifiRssi { get; set; }
        public double? CellVoltage { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Devices Device { get; set; }
    }
}
