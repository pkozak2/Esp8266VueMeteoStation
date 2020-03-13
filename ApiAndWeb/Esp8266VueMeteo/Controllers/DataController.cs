using System;
using Esp8266VueMeteo.Services;
using Microsoft.AspNetCore.Mvc;

namespace VueStartingProject.Controllers
{
    [Obsolete]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly IDevicesService _devicesService;
        private readonly IMeasurementsService _measurementsService;
        public DataController(IDevicesService devicesService, IMeasurementsService measurementsService)
        {
            _devicesService = devicesService;
            _measurementsService = measurementsService;
        }
        [HttpGet("sensors")]
        public IActionResult GetSensors()
        {
            return new JsonResult(_devicesService.GetAllDevices());
        }

        [HttpGet("sensors/{userName}")]
        public IActionResult GetUserSensors(string userName)
        {
            return new JsonResult(_devicesService.GetUserDevices());
        }

        [HttpGet("sensors/esp/{espId}")]
        public IActionResult GetSensorsByEspId(string espId)
        {
            return new JsonResult(_devicesService.GetUserDevices(espId));
        }

        [HttpGet("measurements/{deviceId}")]
        public IActionResult GetCurrentMeasurementsForDevice(Guid deviceId)
        {
            return new JsonResult(_measurementsService.CurrentMeasurementsForDevice(deviceId));
        }

        [HttpGet("measurements/{deviceId}/{hours}")]
        public IActionResult GetMeasurementsForDeviceFromHours(Guid deviceId, int hours)
        {
            return new JsonResult(_measurementsService.MeasurementsForDeviceFromHours(deviceId, hours));
        }
    }
}
