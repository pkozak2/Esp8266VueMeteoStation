using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Esp8266VueMeteo.Database.Models
{
    public class Aggregates
    {
        public Aggregates()
        {
            AggregateId = Guid.NewGuid();
            InsertDateTime = DateTimeOffset.Now;
        }
        [Key]
        public Guid AggregateId { get; set; }
        [Required]
        public Guid DeviceId { get; set; }
        [Required]
        public DateTimeOffset InsertDateTime { get; set; }
        [Required]
        public int Resolution { get; set; }
        public double? Pm25 { get; set; }
        public double? Pm10 { get; set; }
        public double? Co2 { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? HeaterTemperature { get; set; }
        public double? HeaterHumidity { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Devices Device { get; set; }
    }
}
