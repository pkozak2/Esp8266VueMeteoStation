using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Repositories
{
    public interface IJsonUpdatesRepository
    {
        bool CreateJsonUpdate(Guid deviceId, string data);
        Task<bool> CreateJsonUpdateAsync(Guid deviceId, string data);
        bool DeleteOldUpdates(Guid deviceId);
        IEnumerable<JsonUpdates> GetJsonUpdates(Guid deviceId, int top = 10);
        IEnumerable<JsonUpdates> GetJsonUpdates(Guid deviceId);
    }
    public class JsonUpdatesRepository : IJsonUpdatesRepository
    {
        private readonly ILogger _logger;
        private readonly MeteoDbContext _context;
        private readonly AppSettings _settings;
        public JsonUpdatesRepository(MeteoDbContext context, ILogger<JsonUpdatesRepository> logger, AppSettings settings)
        {
            _logger = logger;
            _context = context;
            _settings = settings;
        }

        public IEnumerable<JsonUpdates> GetJsonUpdates(Guid deviceId, int top = 10)
        {
            _logger.LogInformation($"GetJsonUpdates, deviceId: {deviceId}, top: {top}");
            try
            {
                return _context.JsonUpdates.Where(w => w.DeviceId == deviceId).OrderByDescending(o => o.InsertDateTime).Take(top).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetJsonUpdates");
                return Enumerable.Empty<JsonUpdates>();
            }
        }

        public IEnumerable<JsonUpdates> GetJsonUpdates(Guid deviceId)
        {
            _logger.LogInformation($"GetJsonUpdates, deviceId: {deviceId}");
            try
            {
                return _context.JsonUpdates.Where(w => w.DeviceId == deviceId).OrderByDescending(o => o.InsertDateTime).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetJsonUpdates");
                return Enumerable.Empty<JsonUpdates>();
            }
        }

        public bool CreateJsonUpdate(Guid deviceId, string data)
        {
            _logger.LogInformation("Save JSON DATA");
            try
            {

                _context.JsonUpdates.Add(new JsonUpdates() { DeviceId = deviceId, InsertDateTime = DateTimeOffset.Now, JsonValue = data });
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Save JSON DATA");
                return false;
            }
        }

        public async Task<bool> CreateJsonUpdateAsync(Guid deviceId, string data)
        {
            _logger.LogInformation("Save JSON DATA");
            try
            {

                _context.JsonUpdates.Add(new JsonUpdates() { DeviceId = deviceId, InsertDateTime = DateTimeOffset.Now, JsonValue = data });
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Save JSON DATA");
                return false;
            }
        }

        public bool DeleteOldUpdates(Guid deviceId)
        {
            try
            {
                if (_settings.StoreRawDataForHours == 0) return true;
                _logger.LogInformation($"Old data will be deleted for deviceId: {deviceId}");
                var maxDateToDelete = DateTimeOffset.Now.AddHours(-_settings.StoreRawDataForHours);
                var values = _context.JsonUpdates.Where(w => w.DeviceId == deviceId && w.InsertDateTime < maxDateToDelete);
                _context.JsonUpdates.RemoveRange(values);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when DeleteOldUpdates");
                return false;
            }
        }
    }
}
