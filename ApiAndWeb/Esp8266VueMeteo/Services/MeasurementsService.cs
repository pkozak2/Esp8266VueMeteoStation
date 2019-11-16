using Esp8266VueMeteo.Database.Models;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Models.ApiModels;
using Esp8266VueMeteo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Esp8266VueMeteo.Services
{
    public interface IMeasurementsService
    {
        List<Measurements> GetAllMeasurements();
        bool AddSensorMeasurement(Guid deviceId, IList<SensorData> data);
        SensorMeasurementModel CurrentMeasurementsForDevice(Guid deviceId);
        List<SensorsCurrentMeasurements> GetAllCurrentMeasurements();
        List<SensorMeasurementModel> MeasurementsForDeviceFromHours(Guid deviceId, int hours);
    }
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        private readonly IDevicesService _devicesService;
        public MeasurementsService(IMeasurementsRepository measurementsRepository, IDevicesService devicesService)
        {
            _measurementsRepository = measurementsRepository;
            _devicesService = devicesService;
        }
        public bool AddSensorMeasurement(Guid deviceId, IList<SensorData> data)
        {
            var mapping = SensorTypes.ValueMapping;
            MeasurementModel measurement = new MeasurementModel();

            foreach (var d in data)
            {
                var key = mapping.FirstOrDefault(x => x.Value.Contains(d.ValueType)).Key;
                if (string.IsNullOrEmpty(key)) continue;

                PropertyInfo propertyInfo = measurement.GetType().GetProperty(key);

                if (propertyInfo != null)
                {
                    Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                    var safeValue = (d.Value == null) ? null : Convert.ChangeType(d.Value.Replace(".",","), t);
                    propertyInfo.SetValue(measurement, safeValue, null);
                }
            }

            return _measurementsRepository.AddSensorMeasurement(deviceId, measurement.Pm25, measurement.Pm10,
                measurement.Temperature, measurement.Humidity, measurement.Pressure, measurement.HeaterTemperature, measurement.HeaterHumidity, measurement.WifiRssi, measurement.CellVoltage);

        }

        public SensorMeasurementModel CurrentMeasurementsForDevice(Guid deviceId)
        {
            var data = _measurementsRepository.CurrentMeasurementsForDevice(deviceId);
            if(data == null)
            {
                return null;
            }
            return new SensorMeasurementModel() {
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
            };
        }

        public List<SensorsCurrentMeasurements> GetAllCurrentMeasurements()
        {
            var result = new List<SensorsCurrentMeasurements>();
            var devices = _devicesService.GetAllDevices();
            foreach(var device in devices)
            {
                var data = new SensorsCurrentMeasurements();
                data.Sensor = device;
                var measurements = CurrentMeasurementsForDevice(device.SensorId);
                if (measurements == null) continue;
                data.Measurements = measurements;

                result.Add(data);
            }
            return result;
        }

        public List<Measurements> GetAllMeasurements()
        {
            return _measurementsRepository.GetAllMeasurements();
        }

        public List<SensorMeasurementModel> MeasurementsForDeviceFromHours(Guid deviceId, int hours)
        {
            var items = _measurementsRepository.MeasurementsForDeviceFromHours(deviceId, hours);
            if (items == null || !items.Any())
            {
                return null;
            }
            var result = new List<SensorMeasurementModel>();
            foreach(var data in items)
            {
                result.Add(new SensorMeasurementModel()
                {
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
                });
            }

            return result;
        }
    }
}
