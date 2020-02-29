using Esp8266VueMeteo.Database;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Esp8266VueMeteo.Repositories
{
    public interface IJsonUpdatesRepository
    {
        bool SaveUpdate<T>(Guid deviceId, T data);
        bool SaveUpdate(Guid deviceId, string data);
    }
    public class JsonUpdatesRepository : IJsonUpdatesRepository
    {
        private readonly ILogger _logger;
        private readonly MeteoDbContext _context;
        public JsonUpdatesRepository(MeteoDbContext context, ILogger<JsonUpdatesRepository> logger)
        {
            _logger = logger;
            _context = context;
        }
        public bool SaveUpdate<T>(Guid deviceId, T data)
        {
            _logger.LogInformation("Save JSON DATA");
            try
            {
                var jsonData = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                _context.JsonUpdates.Add(new Database.Models.JsonUpdates() { DeviceId = deviceId, InsertDateTime = DateTimeOffset.Now, JsonValue = jsonData });
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Save JSON DATA");
                return false;
            }
        }

        public bool SaveUpdate(Guid deviceId, string data)
        {
            _logger.LogInformation("Save JSON DATA");
            try
            {
               
                _context.JsonUpdates.Add(new Database.Models.JsonUpdates() { DeviceId = deviceId, InsertDateTime = DateTimeOffset.Now, JsonValue = data });
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Save JSON DATA");
                return false;
            }
        }
    }
}
