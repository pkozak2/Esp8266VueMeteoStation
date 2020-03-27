using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class TemperatureGraphData : IGraphData
    {
        public DateTimeOffset? FromDate => TemperatureData.FirstOrDefault()?.DateTime ?? HeaterData.FirstOrDefault()?.DateTime;

        public DateTimeOffset? ToDate => TemperatureData.LastOrDefault()?.DateTime ?? HeaterData.LastOrDefault()?.DateTime;

        public List<GraphDataModel> TemperatureData { get; set; }
        public List<GraphDataModel> HeaterData { get; set; }
        public TemperatureGraphData()
        {
            TemperatureData = new List<GraphDataModel>();
            HeaterData = new List<GraphDataModel>();
        }
    }
}
