using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Esp8266VueMeteo.Repositories
{
    public interface IMeasurementsRepository
    {
        bool AddSensorMeasurement(Guid deviceId, double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity);
    }
    public class MeasurementsRepository : IMeasurementsRepository
    {
        private readonly ILogger _logger;
        private readonly MeteoDbContext _context;
        private readonly IJsonUpdatesRepository _jsonUpdatesRepository;
        public MeasurementsRepository(MeteoDbContext context, IJsonUpdatesRepository jsonUpdatesRepository, ILogger<MeasurementsRepository> logger)
        {
            _logger = logger;
            _context = context;
            _jsonUpdatesRepository = jsonUpdatesRepository;
        }

        public bool AddSensorMeasurement(Guid deviceId, double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity)
        {
            _logger.LogInformation($"In Add sensor measurement for device id: {deviceId}");
            try
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
                _jsonUpdatesRepository.SaveUpdate(deviceId, measurement);
                _context.Measurements.Add(measurement);
                return _context.SaveChanges() >= 1;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error when saving measurement data!");
                return false;
            }
        }
    }
}
