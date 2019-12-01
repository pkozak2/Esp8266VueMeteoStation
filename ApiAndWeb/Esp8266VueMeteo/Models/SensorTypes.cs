using System.Collections.Generic;

namespace Esp8266VueMeteo.Models
{
    public static class SensorTypes
    {
        public static Dictionary<string, List<string>> ValueMapping = new Dictionary<string, List<string>>
        {
            {"Pm1", new List<string>{ "PMS_P0" } },
            {"Pm10", new List<string>{ "SDS_P1", "PMS_P1", "HPM_P1" }},
            {"Pm25", new List<string>{ "SDS_P2", "PMS_P2", "HPM_P2" } },
            {"Temperature", new List<string>{ "BME280_temperature", "BMP_temperature", "DS18B20_temperature", "BMP280_temperature" } },
            {"Humidity", new List<string>{"BME280_humidity"} },
            {"Pressure", new List<string>{"BME280_pressure", "BMP_pressure", "BMP280_pressure"} },
            {"HeaterTemperature", new List<string>{ "temperature", "HECA_temperature" } },
            {"HeaterHumidity", new List<string>{ "humidity", "HECA_humidity" } },
            {"GpsTime", new List<string>{"GPS_time"} },
            {"GpsDate", new List<string>{"GPS_date"} },
            {"WifiRssi", new List<string>{ "wifi_rssi" } },
            {"CellVoltage", new List<string>{ "cell_voltage" } },

        };
    }
}
