namespace Esp8266VueMeteo.Models
{
    public class MeasurementModel : BaseMeasurementModel
    {
        public double? GpsTime { get; set; }
        public double? GpsDate { get; set; }
        public double? WifiRssi { get; set; }
        public double? CellVoltage { get; set; }

    }
}
