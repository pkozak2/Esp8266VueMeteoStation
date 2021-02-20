using Esp8266VueMeteo.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IAqiEcoService
    {
        Task<bool> SendToAqi(Guid deviceId, string aqiEcoUpdateUrl, string requestString);
    }
    public class AqiEcoService : IAqiEcoService
    {
        private readonly ILogger<AqiEcoService> _logger;
        private readonly AppSettings _settings;

        public AqiEcoService(ILogger<AqiEcoService> logger, AppSettings settings)
        {
            _logger = logger;
            _settings = settings;
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
