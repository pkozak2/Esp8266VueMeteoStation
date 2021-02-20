using System;
using System.Collections.Generic;
using System.Linq;
using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Esp8266VueMeteo.Models;
using Microsoft.Extensions.Logging;

namespace Esp8266VueMeteo.Repositories
{
    public interface IMeasurementsRepository
    {
        bool AddSensorMeasurement(Guid deviceId, MeasurementModel measurement);
        List<Measurements> GetAllMeasurements();
        Measurements CurrentMeasurementsForDevice(Guid deviceId);
        List<Measurements> MeasurementsForDeviceFromHours(Guid deviceId, int hours);
    }
    public class MeasurementsRepository : IMeasurementsRepository
    {
        private readonly ILogger _logger;
        private readonly MeteoDbContext _context;
        public MeasurementsRepository(MeteoDbContext context, ILogger<MeasurementsRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public bool AddSensorMeasurement(Guid deviceId, MeasurementModel measurement)
        {
            _logger.LogInformation($"In Add sensor measurement for device id: {deviceId}");
            try
            {
                var dbMeasurement = new Measurements()
                {
                    DeviceId = deviceId,
                    InsertDateTime = DateTimeOffset.Now,
                    Pm25 = measurement.Pm25,
                    Pm10 = measurement.Pm10,
                    Pm1 = measurement.Pm1,
                    Pm4 = measurement.Pm4,
                    N1 = measurement.N1,
                    N25 = measurement.N25,
                    N4 = measurement.N4,
                    N10 = measurement.N10,
                    Co2 = measurement.Co2,
                    Temperature = measurement.Temperature,
                    Humidity = measurement.Humidity,
                    Pressure = measurement.Pressure,
                    HeaterTemperature = measurement.HeaterTemperature,
                    HeaterHumidity = measurement.HeaterHumidity,
                    WifiRssi = measurement.WifiRssi,
                    CellVoltage = measurement.CellVoltage,
                };
                _context.Measurements.Add(dbMeasurement);
                return _context.SaveChanges() >= 1;
            }
            catch (Exception ex)
            {
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
                return _context.Measurements.OrderByDescending(o => o.InsertDateTime).Where(w => w.InsertDateTime >= minDate && w.DeviceId == deviceId).ToList();
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
