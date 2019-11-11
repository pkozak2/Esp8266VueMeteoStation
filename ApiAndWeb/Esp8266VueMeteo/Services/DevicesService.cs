using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IDevicesService
    {
        bool AuthorizeSensor(string deviceId, string httpContextUserName, string httpContextPassword);
    }
    public class DevicesService : IDevicesService
    {
        private readonly IDevicesRepository _devicessRepository;
        public DevicesService(IDevicesRepository devicesRepository)
        {
            _devicessRepository = devicesRepository;
        }
        public bool AuthorizeSensor(string deviceId, string httpContextUserName, string httpContextPassword);
        {
            //TODO: All data and save

            //_measurementsRepository.AddSensorMeasurement(deviceId, null, null, null, null, null, null, null);
            return true;
        }
    }
}
