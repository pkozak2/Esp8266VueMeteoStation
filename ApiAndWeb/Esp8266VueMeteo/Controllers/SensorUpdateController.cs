using System;
using System.Net.Http.Headers;
using System.Text;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esp8266VueMeteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorUpdateController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly IMeasurementsService _measurementsService;
        public SensorUpdateController(IDevicesService devicesService, IMeasurementsService measurementsService)
        {
            _devicesService = devicesService;
            _measurementsService = measurementsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok("OK!");
        }

        [HttpPost]
        public IActionResult UpdateData([FromBody]SensorUpdateRequest request)
        {
            string password;
            string userName;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                userName = credentials[0];
                password = credentials[1];
            }
            catch
            {
                return Unauthorized("Invalid Auth Header!");
            }

            var deviceId = _devicesService.AuthorizeSensor(request.EspId, userName, password);
            if (!deviceId.HasValue || deviceId == Guid.Empty)
            {
                return Unauthorized("Invalid UserName/Password or Device not found!");
            }

            var result = _measurementsService.AddSensorMeasurement(deviceId.Value, request.DataValues);
            return Ok(result);
        }
    }
}