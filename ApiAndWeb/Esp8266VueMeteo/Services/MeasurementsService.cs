using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IMeasurementsService
    {
        bool AddSensorMeasurement(Guid deviceId, List<SensorData> data);
    }
    public class MeasurementsService : IMeasurementsService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        public MeasurementsService(IMeasurementsRepository measurementsRepository)
        {
            _measurementsRepository = measurementsRepository;
        }
        public bool AddSensorMeasurement(Guid deviceId, List<SensorData> data)
        {
            //TODO: All data and save

            _measurementsRepository.AddSensorMeasurement(deviceId, null, null, null, null, null, null, null);
            return true;
        }
    }
}
