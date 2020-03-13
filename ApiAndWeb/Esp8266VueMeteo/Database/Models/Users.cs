using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Esp8266VueMeteo.Database.Models
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        [Required, MaxLength(256)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public DateTime InsertDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public IEnumerable<Devices> Devices { get; set; }
    }
}
