using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class DataJsonModel
    {
        public LastDataJsonModel Last_data { get; set; }
        public AverageDataJsonModel Average_1h { get; set; }
        public AverageDataJsonModel Average_24h { get; set; }

        public DataJsonModel()
        {
            Last_data = new LastDataJsonModel();
            Average_1h = new AverageDataJsonModel();
            Average_24h = new AverageDataJsonModel();
        }
    }

    public class LastDataJsonModel : BaseMeasurementModel
    {
        [JsonProperty(PropertyName = "last_update")]
        public int LastUpdate { get; set; }
    }

    public class AverageDataJsonModel : BaseMeasurementModel
    {
        public string Index { get; set; }
        [JsonProperty(PropertyName = "index_num")]
        public int IndexNum { get; set; }
    }
}
