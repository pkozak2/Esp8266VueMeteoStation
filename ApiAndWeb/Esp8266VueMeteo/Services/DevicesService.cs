using Esp8266VueMeteo.Models.ApiModels;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Services
{
    public interface IDevicesService
    {
        List<SensorModel> GetAllDevices();
        Guid? AuthorizeSensor(string deviceId, string httpContextUserName, string httpContextPassword);
    }
    public class DevicesService : IDevicesService
    {
        private readonly ILogger _logger;
        private readonly IDevicesRepository _devicessRepository;
        public DevicesService(IDevicesRepository devicesRepository, ILogger<DevicesService> logger)
        {
            _logger = logger;
            _devicessRepository = devicesRepository;
        }
        public Guid? AuthorizeSensor(string deviceId, string httpContextUserName, string httpContextPassword)
        {
            _logger.LogInformation($"Authorize sensor: {deviceId}");
            return _devicessRepository.GetDevicesByEspId(deviceId).Where(w => w.HttpUserName == httpContextUserName && w.HttpPassword == httpContextPassword).FirstOrDefault()?.DeviceId;
        }

        public List<SensorModel> GetAllDevices()
        {
            var devices = _devicessRepository.GetAllActiveDevices();
            return  devices.Select(s => new SensorModel() { SensorId = s.DeviceId, SensorDescription = s.Description, SensorDescritionExtra = s.ExtraDescription, SensorName = s.DeviceName }).ToList();
        }
    }
}
