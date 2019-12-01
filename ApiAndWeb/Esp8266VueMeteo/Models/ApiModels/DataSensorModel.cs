using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Models.ApiModels
{
    public class DataSensorModel : SensorModel
    {
        public SensorMeasurementModel Measurements { get; set; }
    }
}
