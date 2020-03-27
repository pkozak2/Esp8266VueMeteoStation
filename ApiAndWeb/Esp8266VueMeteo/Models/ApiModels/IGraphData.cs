using System;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public interface IGraphData
    {
         DateTimeOffset? FromDate { get; }
         DateTimeOffset? ToDate { get; }
    }
}
