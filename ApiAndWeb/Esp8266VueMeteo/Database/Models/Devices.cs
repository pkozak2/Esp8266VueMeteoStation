using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Esp8266VueMeteo.Database.Models
{
    public class Devices
    {
        public Devices()
        {
            DeviceId = Guid.NewGuid();
            IsActive = true;
            LocationProvided = false;
            InsertDateTime = DateTimeOffset.Now;
        }
        [Key]
        public Guid DeviceId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsDefault { get; set; }
        [Required]
        public string Esp8266Id { get; set; }
        [Required, MaxLength(256)]
        public string HttpUserName { get; set; }
        [Required, MaxLength(256)]
        public string HttpPassword { get; set; }
        [Required, MaxLength(256)]
        public string DeviceName { get; set; }
        [Required, MaxLength(256)]
        public string DeviceNormalizedName { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string ExtraDescription { get; set; }
        [Required]
        public bool LocationProvided { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public int? Radius { get; set; }
        [Required]
        public DateTimeOffset InsertDateTime { get; set; }
        public bool IsActive { get; set; }

        public bool SendToAqiEco { get; set; }
        public string AqiEcoUpdateUrl { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Measurements> Measurements { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Users User { get; set; }
    }
}
