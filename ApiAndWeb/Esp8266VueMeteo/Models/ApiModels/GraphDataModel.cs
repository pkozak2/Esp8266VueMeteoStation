using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class GraphDataModel
    {
        public DateTimeOffset? DateTime { get; set; }
        public double? Value { get; set; }
    }
}
