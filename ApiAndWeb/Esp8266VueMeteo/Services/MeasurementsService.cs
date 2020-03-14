using Esp8266VueMeteo.Database.Models;
using Esp8266VueMeteo.Helpers;
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
        List<SensorMeasurementModel> MeasurementsForDeviceFromHours(Guid deviceId, int hours);
        DataJsonModel GetCurrentDataJson(Guid deviceId);
        AveragesModel AverageMeasurementsForDevice(Guid deviceId);
    }
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        private readonly IDevicesRepository _devicesReporistory;
        public MeasurementsService(IMeasurementsRepository measurementsRepository, IDevicesRepository devicesRepository)
        {
            _devicesReporistory = devicesRepository;
            _measurementsRepository = measurementsRepository;
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

        public AveragesModel AverageMeasurementsForDevice(Guid deviceId)
        {
            var result = new AveragesModel();
            var measurements = MeasurementsForDeviceFromHours(deviceId, 24).OrderBy(o => o.InsertDate);

            var last1h = measurements.Where(w => w.InsertDate >= DateTimeOffset.Now.AddHours(-1));
            if (last1h.Any())
            {
                var averagePm25 = last1h.Where(w => w.Pm25 != null).Average(a => a.Pm25);
                var averagePm10 = last1h.Where(w => w.Pm10 != null).Average(a => a.Pm10);

                if (averagePm10 != null || averagePm25 != null)
                {

                    var percentpm25 = Math.Round(100 * ((averagePm25 ?? 0) / AirQualityHelper.PM25Limit1h), 0);
                    var percentpm10 = Math.Round(100 * ((averagePm10 ?? 0) / AirQualityHelper.PM10Limit1h), 0);

                    result.Average_1h.Add(new AverageDataModel()
                    {
                        Name = "PM",
                        SubName = "2.5",
                        Value = averagePm25.HasValue ? Math.Round(averagePm25.Value, 0) : (double?)null,
                        Percent = (int)percentpm25,
                        Index = AirQualityHelper.FindLevel(AirQualityHelper.PM25Thresholds1h, averagePm25).Index
                    });

                    result.Average_1h.Add(new AverageDataModel()
                    {
                        Name = "PM",
                        SubName = "10",
                        Value = averagePm10.HasValue ? Math.Round(averagePm10.Value, 0) : (double?)null,
                        Percent = (int)percentpm10,
                        Index = AirQualityHelper.FindLevel(AirQualityHelper.PM10Thresholds1h, averagePm10).Index
                    });
                }
            }
            if (measurements.Any())
            {
                var averagePm25 = measurements.Where(w => w.Pm25 != null).Average(a => a.Pm25);
                var averagePm10 = measurements.Where(w => w.Pm10 != null).Average(a => a.Pm10);
                if (averagePm10 != null || averagePm25 != null)
                {

                    var percentpm25 = Math.Round(100 * ((averagePm25 ?? 0) / AirQualityHelper.PM25Limit24h), 0);
                    var percentpm10 = Math.Round(100 * ((averagePm10 ?? 0) / AirQualityHelper.PM10Limit24h), 0);


                    result.Average_24h.Add(new AverageDataModel()
                    {
                        Name = "PM",
                        SubName = "2.5",
                        Value = averagePm25.HasValue ? Math.Round(averagePm25.Value, 0) : (double?)null,
                        Percent = (int)percentpm25,
                        Index = AirQualityHelper.FindLevel(AirQualityHelper.PM25Thresholds24h, averagePm25).Index
                    });

                    result.Average_24h.Add(new AverageDataModel()
                    {
                        Name = "PM",
                        SubName = "10",
                        Value = averagePm10.HasValue ? Math.Round(averagePm10.Value, 0) : (double?)null,
                        Percent = (int)percentpm10,
                        Index = AirQualityHelper.FindLevel(AirQualityHelper.PM10Thresholds24h, averagePm10).Index
                    });
                }
            }
            return result;

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

        public List<Measurements> GetAllMeasurements()
        {
            return _measurementsRepository.GetAllMeasurements();
        }

        public DataJsonModel GetCurrentDataJson(Guid deviceId)
        {
            var result = new DataJsonModel();
            var measurements = MeasurementsForDeviceFromHours(deviceId, 24).OrderBy(o => o.InsertDate);
            var current = measurements.FirstOrDefault();
            if (current != null)
            {
                result.Last_data = new LastDataJsonModel
                {
                    LastUpdate = (int)current.InsertDate.Value.UtcDateTime.Subtract(DateTime.UnixEpoch).TotalSeconds,
                    HeaterHumidity = current.HeaterHumidity,
                    HeaterTemperature = current.HeaterTemperature,
                    Humidity = current.Humidity,
                    Pm10 = current.Pm10,
                    Pm25 = current.Pm25,
                    Pressure = current.Pressure,
                    Temperature = current.Temperature
                };
            }
            var last1h = measurements.Where(w => w.InsertDate >= DateTimeOffset.Now.AddHours(-1));
            if (last1h.Any())
            {
                var index = AirQualityHelper.FindLevel(AirQualityHelper.PM10Thresholds1h, last1h.Average(a => a.Pm10));
                result.Average_1h = new AverageDataJsonModel
                {
                    Pm25 = last1h.Average(a => a.Pm25),
                    Pm10 = last1h.Average(a => a.Pm10),
                    Temperature = last1h.Average(a => a.Temperature),
                    Pressure = last1h.Average(a => a.Pressure),
                    Humidity = last1h.Average(a => a.Humidity),
                    HeaterTemperature = last1h.Average(a => a.HeaterTemperature),
                    HeaterHumidity = last1h.Average(a => a.HeaterHumidity),
                    Index = index.Name,
                    IndexNum = index.Index
                };
            }
            if (measurements.Any())
            {
                var index = AirQualityHelper.FindLevel(AirQualityHelper.PM10Thresholds24h, measurements.Average(a => a.Pm10));
                result.Average_24h = new AverageDataJsonModel
                {
                    Pm25 = measurements.Average(a => a.Pm25),
                    Pm10 = measurements.Average(a => a.Pm10),
                    Temperature = measurements.Average(a => a.Temperature),
                    Pressure = measurements.Average(a => a.Pressure),
                    Humidity = measurements.Average(a => a.Humidity),
                    HeaterTemperature = measurements.Average(a => a.HeaterTemperature),
                    HeaterHumidity = measurements.Average(a => a.HeaterHumidity),
                    Index = index.Name,
                    IndexNum = index.Index
                };
            }
            return result;
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
