using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Esp8266VueMeteo.Database.Models
{
    public class JsonUpdates
    {
        public JsonUpdates()
        {
            JsonUpdateId = Guid.NewGuid();
            InsertDateTime = DateTimeOffset.Now;
        }
        [Key]
        public Guid JsonUpdateId { get; set; }
        [Required]
        public Guid DeviceId { get; set; }
        [Required]
        public DateTimeOffset InsertDateTime { get; set; }
        [Required, MaxLength(5000)]
        public string JsonValue { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Devices Device { get; set; }
    }
}
