using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class AverageDataModel
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public double? Value { get; set; }
        public int Percent { get; set; }
        public int Index { get; set; }
    }
    public class AveragesModel
    {
        public List<AverageDataModel> Average_1h { get; set; }
        public List<AverageDataModel> Average_24h { get; set; }

        public AveragesModel()
        {
            Average_1h = new List<AverageDataModel>();
            Average_24h = new List<AverageDataModel>();
        }
    }
}
