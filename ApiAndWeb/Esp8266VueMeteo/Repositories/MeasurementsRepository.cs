using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Repositories
{
    public interface IMeasurementsRepository
    {
        bool AddSensorMeasurement(Guid deviceId, double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity, double? rssi, double? cellVoltage);
        List<Measurements> GetAllMeasurements();
        Measurements CurrentMeasurementsForDevice(Guid deviceId);
        List<Measurements> MeasurementsForDeviceFromHours(Guid deviceId, int hours);
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

        public bool AddSensorMeasurement(Guid deviceId, double? pm25, double? pm10, double? temperature, double? humidity, double? pressure, double? heaterTemperature, double? heaterHumidity, double? rssi, double? cellVoltage)
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
                    Temperature = temperature,
                    CellVoltage = cellVoltage,
                    WifiRssi = rssi
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

        public Measurements CurrentMeasurementsForDevice(Guid deviceId)
        {
            _logger.LogInformation($"CurrentMeasurementsForDevice: {deviceId}");
            try
            {
                return _context.Measurements.OrderByDescending(o => o.InsertDateTime).FirstOrDefault(w => w.DeviceId == deviceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when CurrentMeasurementsForDevice!");
                return null;
            }
        }

        public List<Measurements> MeasurementsForDeviceFromHours(Guid deviceId, int hours)
        {
            _logger.LogInformation($"MeasurementsForDeviceFromHours: {deviceId}, from {hours} behind");
            try
            {
                var minDate = DateTimeOffset.Now.AddHours(-hours);
                return _context.Measurements.OrderByDescending(o => o.InsertDateTime).Where(w => w.InsertDateTime >= minDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when MeasurementsForDeviceFromHours!");
                return new List<Measurements>();
            }
        }

        public List<Measurements> GetAllMeasurements()
        {
           return _context.Measurements.OrderByDescending(o => o.InsertDateTime).ToList();
        }
    }
}
