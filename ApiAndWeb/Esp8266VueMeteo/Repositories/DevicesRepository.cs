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
        List<Devices> GetAllActiveDevices();
        List<Devices> GetDevicesByEspId(string espId);
        Devices GetDeviceById(Guid deviceId);
        List<Devices> GetUserDevices(Guid userId);
        Devices GetDeviceByNormalizedName(string normalizedName);
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

        public List<Devices> GetAllActiveDevices()
        {
            try
            {
                return _context.Devices.Where(w => w.IsActive).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetAllActiveDevices");
                return new List<Devices>();
            }
        }

        public Devices GetDeviceById(Guid deviceId)
        {
            try
            {
                return _context.Devices.FirstOrDefault(w => w.DeviceId == deviceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetDeviceById");
                return null;
            }
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

        public List<Devices> GetUserDevices(Guid userId)
        {
            try
            {
                return _context.Devices.Where(w => w.IsActive && w.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetUserDevices");
                return new List<Devices>();
            }
        }

        public Devices GetDeviceByNormalizedName(string normalizedName)
        {
            try
            {
                return _context.Devices.FirstOrDefault(d => d.DeviceNormalizedName == normalizedName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when GetDeviceByNormalizedName");
                return null;
            }
        }
    }
}