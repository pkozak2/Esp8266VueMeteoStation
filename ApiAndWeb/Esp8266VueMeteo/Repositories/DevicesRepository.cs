using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Database.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Repositories
{
    public interface IDevicesRepository
    {
        List<Devices> GetDevicesByEspId(string espId);
    }
    public class DevicesRepository : IDevicesRepository
    {
        private readonly ILogger _logger;
        private readonly MeteoDbContext _context;
        public DevicesRepository(MeteoDbContext context, ILogger<DevicesRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public List<Devices> GetDevicesByEspId(string espId)
        {
            try
            {
                return _context.Devices.Where(d => espId.Contains(d.Esp8266Id) && d.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetdevicesByEspId");
                return new List<Devices>();
            }
        }
    }
}