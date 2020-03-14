using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class DeviceModel
    {
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public string DeviceDescritionExtra { get; set; }

    }
}
