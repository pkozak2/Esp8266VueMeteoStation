using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;

namespace Esp8266VueMeteo.Services
{
    public interface ISensorUpdateService
    {
        Task<bool> UpdateSensorRecordsAsync(Guid deviceId, IList<SensorData> data, string requestString);
    }
    public class SensorUpdateService : ISensorUpdateService
    {
        private readonly ILogger<SensorUpdateService> _logger;
        private readonly IMeasurementsService _measurementsService;
        private readonly IJsonUpdatesService _jsonUpdatesService;
        private readonly IAqiEcoService _aqiEcoService;
        private readonly IDevicesRepository _devicesRepository;

        public SensorUpdateService(ILogger<SensorUpdateService> logger, IMeasurementsService measurementsService, IJsonUpdatesService jsonUpdatesService, IAqiEcoService aqiEcoService, IDevicesRepository devicesRepository )
        {
            _logger = logger;
            _measurementsService = measurementsService;
            _jsonUpdatesService = jsonUpdatesService;
            _aqiEcoService = aqiEcoService;
            _devicesRepository = devicesRepository;
        }

        public async Task<bool> UpdateSensorRecordsAsync(Guid deviceId, IList<SensorData> data, string requestString)
        {
            _logger.LogInformation($"{nameof(SensorUpdateService)}.{nameof(UpdateSensorRecordsAsync)} for deviceId: {deviceId}, requestString: {requestString}");
            var device = _devicesRepository.GetDeviceById(deviceId);

            if (device == null) return false;

            var tasks = new List<Task<bool>>();

            if(device.SendToAqiEco && !string.IsNullOrEmpty(device.AqiEcoUpdateUrl))
            {
                tasks.Add(_aqiEcoService.SendToAqi(deviceId, device.AqiEcoUpdateUrl, requestString));
            }

            tasks.Add(_jsonUpdatesService.SaveUpdateAsync(deviceId, requestString));

            tasks.Add(_measurementsService.AddSensorMeasurementAsync(deviceId, data));

            var result = await Task.WhenAll(tasks);
            return result.Any(r => r == true);
        }
    }
}
