using System;
using System.Linq;
using System.Threading.Tasks;
using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Esp8266VueMeteo.Models;
using Microsoft.Extensions.Logging;

namespace Esp8266VueMeteo.Repositories
{
    public interface IRecordsRepository
    {
        Task<bool> AddSensorRecordAsync(Guid deviceId, MeasurementModel model);
        bool DeleteOldRecords(Guid deviceId);

    }
    public class RecordsRepository : IRecordsRepository
    {
        private readonly MeteoDbContext _context;
        private readonly ILogger<RecordsRepository> _logger;
        private readonly AppSettings _settings;

        public RecordsRepository(MeteoDbContext context, ILogger<RecordsRepository> logger, AppSettings settings)
        {
            _context = context;
            _logger = logger;
            _settings = settings;
        }

        public async Task<bool> AddSensorRecordAsync(Guid deviceId, MeasurementModel model)
        {
            _logger.LogInformation($"In Add sensor Records for device id: {deviceId}");
            try
            {
                var measurement = new Records()
                {
                    DeviceId = deviceId,
                    Pm25 = model.Pm25,
                    Pm10 = model.Pm10,
                    Co2 = model.Co2,
                    HeaterHumidity = model.HeaterHumidity,
                    HeaterTemperature = model.HeaterTemperature,
                    Humidity = model.Humidity,
                    InsertDateTime = DateTimeOffset.Now,
                    Pressure = model.Pressure,
                    Temperature = model.Temperature
                };
                _context.Records.Add(measurement);
                return await _context.SaveChangesAsync() >= 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when saving record data!");
                return false;
            }
        }

        public bool DeleteOldRecords(Guid deviceId)
        {
            try
            {
                if (_settings.StoreRawDataForHours == 0) return true;
                _logger.LogInformation($"Old Records will be deleted for deviceId: {deviceId}");
                var maxDateToDelete = DateTimeOffset.Now.AddHours(-_settings.StoreRawDataForHours);
                var values = _context.Records.Where(w => w.DeviceId == deviceId && w.InsertDateTime < maxDateToDelete);
                _context.Records.RemoveRange(values);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when DeleteOldRecords");
                return false;
            }
        }
    }
}

