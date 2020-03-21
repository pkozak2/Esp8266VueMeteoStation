using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class PollutonLevelGraphData
    {
        public DateTimeOffset? FromDate => Pm10Data.FirstOrDefault()?.DateTime ?? Pm25Data.FirstOrDefault()?.DateTime;
        public DateTimeOffset? ToDate => Pm10Data.LastOrDefault()?.DateTime ?? Pm25Data.LastOrDefault()?.DateTime;
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
