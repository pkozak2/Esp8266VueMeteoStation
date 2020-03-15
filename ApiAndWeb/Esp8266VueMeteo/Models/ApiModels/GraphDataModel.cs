using System;
using System.Collections.Generic;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class PollutonLevelGraphData
    {
        public int Pm25Limit { get; set; }
        public int Pm10Limit { get; set; }
        public List<GraphDataModel> Pm10Data { get; set; }
        public List<GraphDataModel> Pm25Data { get; set; }
        public PollutonLevelGraphData()
        {
            Pm10Data = new List<GraphDataModel>();
            Pm25Data = new List<GraphDataModel>();
        }
    }
    public class GraphDataModel
    {
        public DateTimeOffset? DateTime { get; set; }
        public double? Value { get; set; }
    }
}
