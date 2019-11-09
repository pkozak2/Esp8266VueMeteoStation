using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Repositories
{
    public interface IMeasurementsRepository
    {
        bool AddSensorMeasurement(Guid deviceId, double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity);
    }
    public class MeasurementsRepository : IMeasurementsRepository
    {
        private readonly MeteoDbContext _context;
        public MeasurementsRepository(MeteoDbContext context)
        {
            _context = context;
        }

        public bool AddSensorMeasurement(Guid deviceId , double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity)
        {
            var measurement = new Measurements()
            {
                DeviceId = deviceId,
                Pm25 = pm25,
                Pm10 = pm10,
                HeaterHumidity = heaterHumidity,
                HeaterTemperature = heaterTemperature,
                Humidity = humidity,
                InsertDateTime = DateTimeOffset.Now,
                Pressure = pressure,
                Temperature = temperature
            };
            _context.Measurements.Add(measurement);
            return _context.SaveChanges() >= 1;
        }
    }
}
