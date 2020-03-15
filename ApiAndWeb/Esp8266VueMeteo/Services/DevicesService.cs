using Esp8266VueMeteo.Models.ApiModels;
using Esp8266VueMeteo.Models.Base;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Esp8266VueMeteo.Services
{
    public interface IDevicesService
    {
        List<DeviceModel> GetAllDevices();
        Guid? AuthorizeSensor(string deviceId, string httpContextUserName = null, string httpContextPassword = null);
        List<DataSensorModel> GetUserDevices(string deviceId = null);
        DeviceModel GetDeviceData(Guid deviceId);

        //NEW
        Guid? GetDeviceIdByNormalizedName(string normalizedName);
        IEnumerable<DeviceModel> GetUserDevices(Guid userId);
        IEnumerable<DefaultSelectItem<string>> GetUserDevicesSelectModel(Guid userId);
    }
    public class DevicesService : IDevicesService
    {
        private readonly ILogger _logger;
        private readonly IDevicesRepository _devicessRepository;
        private readonly IMeasurementsRepository _measurementsRepository;
        public DevicesService(IDevicesRepository devicesRepository, IMeasurementsRepository measurementsRepository, ILogger<DevicesService> logger)
        {
            _logger = logger;
            _devicessRepository = devicesRepository;
            _measurementsRepository = measurementsRepository;
        }
        public Guid? AuthorizeSensor(string deviceId, string httpContextUserName = null, string httpContextPassword = null)
        {
            _logger.LogInformation($"Authorize sensor: {deviceId}");
            return _devicessRepository.GetDevicesByEspId(deviceId).Where(w => w.HttpUserName == httpContextUserName && w.HttpPassword == httpContextPassword).FirstOrDefault()?.DeviceId;
        }

        public List<DeviceModel> GetAllDevices()
        {
            var devices = _devicessRepository.GetAllActiveDevices();
            return  devices.Select(s => new DeviceModel() { DeviceId = s.DeviceId, DeviceDescription = s.Description, DeviceDescritionExtra = s.ExtraDescription, DeviceName = s.DeviceName }).ToList();
        }

        public List<DataSensorModel> GetUserDevices(string deviceId = null)
        {
            var result = new List<DataSensorModel>();
            var devices = !string.IsNullOrEmpty(deviceId) ? _devicessRepository.GetDevicesByEspId(deviceId) : _devicessRepository.GetAllActiveDevices();
            //var devices = _devicessRepository.GetAllActiveDevices();
            foreach(var s in devices)
            {
                var data = _measurementsRepository.CurrentMeasurementsForDevice(s.DeviceId);
                result.Add(new DataSensorModel() { DeviceId = s.DeviceId,
                    DeviceDescription = s.Description,
                    DeviceDescritionExtra = s.ExtraDescription,
                    DeviceName = s.DeviceName,
                    Measurements = data == null ? new SensorMeasurementModel() : new SensorMeasurementModel() {
                        CellVoltage = data.CellVoltage,
                        HeaterHumidity = data.HeaterHumidity,
                        HeaterTemperature = data.HeaterTemperature,
                        Humidity = data.Humidity,
                        Pm10 = data.Pm10,
                        Pm25 = data.Pm25,
                        Pressure = data.Pressure,
                        Temperature = data.Temperature,
                        WifiRssi = data.WifiRssi,
                        InsertDate = data.InsertDateTime
                    } });
            }
            return result;
        }

        public DeviceModel GetDeviceData(Guid deviceId)
        {
            var device = _devicessRepository.GetDeviceById(deviceId);
            if (device == null) return null;
            return new DeviceModel() { DeviceId = device.DeviceId, DeviceDescription = device.Description, DeviceDescritionExtra = device.ExtraDescription, DeviceName = device.DeviceName };
        }

        public IEnumerable<DeviceModel> GetUserDevices(Guid userId)
        {
            var devices = _devicessRepository.GetUserDevices(userId);

            return devices.Select(s => new DeviceModel() { DeviceId = s.DeviceId, DeviceDescription = s.Description, DeviceDescritionExtra = s.ExtraDescription, DeviceName = s.DeviceName });
        }

        public IEnumerable<DefaultSelectItem<string>> GetUserDevicesSelectModel(Guid userId)
        {
            var devices = _devicessRepository.GetUserDevices(userId);

            return devices.Select(s => new DefaultSelectItem<string>() { Id = s.DeviceNormalizedName, Text = s.DeviceName, IsDefault = s.IsDefault });
        }

        public Guid? GetDeviceIdByNormalizedName(string normalizedName)
        {
            var device = _devicessRepository.GetDeviceByNormalizedName(normalizedName);
            if (device == null) return null;
            return device.DeviceId;
        }
    }
}
