using Esp8266VueMeteo.Helpers;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IAqiEcoService
    {
        void SendToAqi(Guid deviceId, string requestString);
        Task<bool> SendToAqi(Guid deviceId, string aqiEcoUpdateUrl, string requestString);
    }
    public class AqiEcoService : IAqiEcoService
    {
        private readonly ILogger<AqiEcoService> _logger;
        private readonly IDevicesRepository _devicesRepository;
        private readonly AppSettings _settings;

        public AqiEcoService(ILogger<AqiEcoService> logger, IDevicesRepository devicesRepository, AppSettings settings)
        {
            _logger = logger;
            _devicesRepository = devicesRepository;
            _settings = settings;
        }

        [Obsolete]
        public void SendToAqi(Guid deviceId, string requestString)
        {
            _logger.LogInformation($"SendToAqi for device ID: {deviceId}");
            var device = _devicesRepository.GetDeviceById(deviceId);
            if (device != null && device.SendToAqiEco && !string.IsNullOrEmpty(device.AqiEcoUpdateUrl))
            {
                _logger.LogInformation($"SendToAqi - data will be send");
                Task.Run(async () =>
                {
                    var client = new HttpClient();
                    var url = $"{_settings.AqiEcoBaseUrl}{device.AqiEcoUpdateUrl}";
                    var response = await client.PostAsync(url, new JsonContent(requestString));
                    if (response.IsSuccessStatusCode) {
                        _logger.LogInformation($"Code: {response.StatusCode}, {response.ReasonPhrase}");
                    }
                });

            }
        }

        public async Task<bool> SendToAqi(Guid deviceId, string aqiEcoUpdateUrl, string requestString)
        {
            _logger.LogInformation($"SendToAqi for device ID: {deviceId}, and updateUrl: {aqiEcoUpdateUrl}");
            return await Task.Run(async () =>
            {
                var client = new HttpClient();
                var url = $"{_settings.AqiEcoBaseUrl}{aqiEcoUpdateUrl}";
                var response = await client.PostAsync(url, new JsonContent(requestString));
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Code: {response.StatusCode}, {response.ReasonPhrase}");
                }
                return response.IsSuccessStatusCode;
            });
        }
    }
}
