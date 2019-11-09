using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public Devices Device { get; set; }
    }
}
