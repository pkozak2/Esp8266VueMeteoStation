using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Services
{
    public interface IJsonUpdatesService
    {
        void SaveUpdate(Guid deviceId, string jsonUpdate);
    }
    public class JsonUpdatesService : IJsonUpdatesService
    {
        private readonly ILogger _logger;
        private readonly IJsonUpdatesRepository _repository;

        public JsonUpdatesService(IJsonUpdatesRepository repository, ILogger<JsonUpdatesService> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public void SaveUpdate(Guid deviceId, string jsonUpdate)
        {
            _logger.LogInformation($"save update JSON sensor: {deviceId}");
            _repository.CreateJsonUpdate(deviceId, jsonUpdate);
        }
    }
}
