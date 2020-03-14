namespace Esp8266VueMeteo.Models.ApiModels
{
    public class SensorsCurrentMeasurements
    {
        public DeviceModel Sensor { get; set; }
        public SensorMeasurementModel Measurements { get; set; }
    }
}
