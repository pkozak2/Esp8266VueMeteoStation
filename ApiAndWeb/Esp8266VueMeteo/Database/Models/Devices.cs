using System;
using System.ComponentModel.DataAnnotations;

namespace Esp8266VueMeteo.Database.Models
{
    public class Devices
    {
        public Devices()
        {
            IsActive = true;
        }
        [Key]
        public Guid DeviceId { get; set; }
        [Required]
        public string Esp8266Id { get; set; }
        [Required, MaxLength(255)]
        public string DeviceName { get; set; }
        public bool IsActive { get; set; }
    }
}
