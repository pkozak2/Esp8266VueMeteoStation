using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Esp8266VueMeteo.Services
{
    public interface IMeasurementsService
    {
        bool AddSensorMeasurement(Guid deviceId, IList<SensorData> data);
    }
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        public MeasurementsService(IMeasurementsRepository measurementsRepository)
        {
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
                    var safeValue = (d.Value == null) ? null : Convert.ChangeType(d.Value, t);
                    propertyInfo.SetValue(measurement, safeValue, null);
                }
            }

            return _measurementsRepository.AddSensorMeasurement(deviceId, measurement.Pm25, measurement.Pm10,
                measurement.Temperature, measurement.Humidity, measurement.Pressure, measurement.HeaterTemperature, measurement.HeaterHumidity);

        }
    }
}
