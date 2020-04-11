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
        private readonly ISensorUpdateService _sensorUpdateService;

        public SensorUpdateController(IDevicesService devicesService, ISensorUpdateService sensorUpdateService)
        {
            _devicesService = devicesService;
            _sensorUpdateService = sensorUpdateService;
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
            var result = await _sensorUpdateService.UpdateSensorRecordsAsync(deviceId.Value, request.DataValues, requestString);
            return Ok(result);
        }
    }
}