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
        List<DataSensorModel> GetUserDevices(string deviceId = null);
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

        public List<DataSensorModel> GetUserDevices(string deviceId = null)
        {
            var result = new List<DataSensorModel>();
            var devices = !string.IsNullOrEmpty(deviceId) ? _devicessRepository.GetDevicesByEspId(deviceId) : _devicessRepository.GetAllActiveDevices();
            //var devices = _devicessRepository.GetAllActiveDevices();
            foreach(var s in devices)
            {
                var data = _measurementsRepository.CurrentMeasurementsForDevice(s.DeviceId);
                result.Add(new DataSensorModel() { SensorId = s.DeviceId,
                    SensorDescription = s.Description,
                    SensorDescritionExtra = s.ExtraDescription,
                    SensorName = s.DeviceName,
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
    }
}
