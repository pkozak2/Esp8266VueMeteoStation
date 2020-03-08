using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Esp8266VueMeteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorUpdateController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly IMeasurementsService _measurementsService;
        private readonly IJsonUpdatesService _jsonUpdatesService;
        private readonly IAqiEcoService _aqiEcoService;
        public SensorUpdateController(IDevicesService devicesService, IMeasurementsService measurementsService, IJsonUpdatesService jsonUpdatesService, IAqiEcoService aqiEcoService)
        {
            _devicesService = devicesService;
            _measurementsService = measurementsService;
            _jsonUpdatesService = jsonUpdatesService;
            _aqiEcoService = aqiEcoService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok("OK!");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData()
        {
            string password = string.Empty;
            string userName  = string.Empty;
            try
            {
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                    userName = credentials[0];
                    password = credentials[1];
                }
            }
            catch
            {
                return Unauthorized("Invalid Auth Header!");
            }

            string requestString;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                requestString = await reader.ReadToEndAsync();
            }

            var request = JsonConvert.DeserializeObject<SensorUpdateRequest>(requestString);

            var deviceId = _devicesService.AuthorizeSensor(request.EspId, userName, password);
            if (!deviceId.HasValue || deviceId == Guid.Empty)
            {
                return Unauthorized("Invalid UserName/Password or Device not found!");
            }
            _jsonUpdatesService.SaveUpdate(deviceId.Value, requestString);
            _aqiEcoService.SendToAqi(deviceId.Value, requestString);
            var result = _measurementsService.AddSensorMeasurement(deviceId.Value, request.DataValues);
            return Ok(result);
        }
    }
}