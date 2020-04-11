using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;

namespace Esp8266VueMeteo.Services
{
    public interface IRecordsService
    {
        Task<bool> AddRecordAsync(Guid deviceId, IList<SensorData> data);
    }
    public class RecordsService : IRecordsService
    {
        private readonly ILogger<RecordsService> _logger;
        private readonly IRecordsRepository _recordsRepository;

        public RecordsService(ILogger<RecordsService> logger, IRecordsRepository recordsRepository)
        {
            _logger = logger;
            _recordsRepository = recordsRepository;
        }

        public async Task<bool> AddRecordAsync(Guid deviceId, IList<SensorData> data)
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
                    var safeValue = (d.Value == null) ? null : Convert.ChangeType(d.Value.Replace(".", ","), t);
                    propertyInfo.SetValue(measurement, safeValue, null);
                }
            }

            if (measurement.Temperature < -127) measurement.Temperature = null;
            if (measurement.HeaterTemperature < -127) measurement.HeaterTemperature = null;
            if (measurement.Humidity < 0) measurement.Humidity = null;
            if (measurement.HeaterHumidity < 0) measurement.HeaterHumidity = null;
            if (measurement.Pm10 < 0) measurement.Pm10 = null;
            if (measurement.Pm25 < 0) measurement.Pm25 = null;
            if (measurement.Pressure < 0) measurement.Pressure = null;

            _recordsRepository.DeleteOldRecords(deviceId);
            return await _recordsRepository.AddSensorRecordAsync(deviceId, measurement);
        }
    }
}
