using System.Collections.Generic;

namespace Esp8266VueMeteo.Models
{
    public static class SensorTypes
    {
        public static Dictionary<string, List<string>> ValueMapping = new Dictionary<string, List<string>>
        {
            {"Pm10",                new List<string>{"SDS_P1", "PMS_P1",     "HPM_P1",   "SPS30_P1" }},
            {"Pm25",                new List<string>{"SDS_P2", "PMS_P2",     "HPM_P2",   "SPS30_P2" } },
            {"Pm1",                 new List<string>{          "PMS_P0",                 "SPS30_P0" } },
            {"Pm4",                 new List<string>{                                    "SPS30_P4" } },
            {"N05",                 new List<string>{                                    "SPS30_N05" } },
            {"N1",                  new List<string>{                                    "SPS30_N1" } },
            {"N25",                 new List<string>{                                    "SPS30_N25" } },
            {"N4",                  new List<string>{                                    "SPS30_N4" } },
            {"N10",                 new List<string>{                                    "SPS30_N10" } },
            {"Co2",                 new List<string>{"conc_co2_ppm" } },
            {"Temperature",         new List<string>{"BME280_temperature", "BMP_temperature", "BMP280_temperature", "HTU21_temperature", "DHT22_temperature", "SHT1x_temperature" } },
            {"Humidity",            new List<string>{"BME280_humidity", "HTU21_humidity", "DHT22_humidity", "SHT1x_humidity" } },
            {"Pressure",            new List<string>{"BME280_pressure", "BMP_pressure", "BMP280_pressure"} },
            {"HeaterTemperature",   new List<string>{"temperature", "HECA_temperature" } },
            {"HeaterHumidity",      new List<string>{"humidity", "HECA_humidity" } },
            {"GpsTime",             new List<string>{"GPS_time"} },
            {"GpsDate",             new List<string>{"GPS_date"} },
            {"WifiRssi",            new List<string>{"wifi_rssi", "signal" } },
            {"CellVoltage",         new List<string>{"cell_voltage" } },

        };
    }
}
