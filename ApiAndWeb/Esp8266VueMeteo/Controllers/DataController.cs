using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esp8266VueMeteo.Models;
using Esp8266VueMeteo.Models.ApiModels;
using Esp8266VueMeteo.Services;
using Microsoft.AspNetCore.Mvc;

namespace VueStartingProject.Controllers
{
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

        [HttpGet("measurements/all")]
        public IActionResult GetCurrentMeasurementsForDevices()
        {
            return new JsonResult(_measurementsService.GetAllCurrentMeasurements());
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
