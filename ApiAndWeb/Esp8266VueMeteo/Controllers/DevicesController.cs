﻿using Esp8266VueMeteo.Models.ApiModels;
using Esp8266VueMeteo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266VueMeteo.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly IMeasurementsService _measurementsService;

        public DevicesController(IDevicesService devicesService, IMeasurementsService measurementsService)
        {
            _devicesService = devicesService;
            _measurementsService = measurementsService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello from devices!");
        }



        [HttpGet("{deviceId}/data")]
        public IActionResult GetDeviceCurrentData(Guid deviceId)
        {
            var device = _measurementsService.CurrentMeasurementsForDevice(deviceId);
            if (device == null) return NotFound();
            return Ok(device);
        }

        [HttpGet("{deviceId}/averages/{hours}")]
        public IActionResult GetDeviceAverages(Guid deviceId, int hours)
        {
            var device = _measurementsService.AverageMeasurementsForDevice(deviceId, hours);
            if (device == null) return NotFound();
            return Ok(device);
        }

        [HttpGet("{deviceId}/averages/{graphType}/{days}")]
        public IActionResult GetDevicePollutionGraphData(Guid deviceId, GraphTypeEnum graphType, int days)
        {
            if (graphType == 0) return BadRequest("Wrong graph Type!");
            switch (graphType)
            {
                case GraphTypeEnum.Pollution:
                    {
                        var device = _measurementsService.AveragePollutionsForDeviceGraph(deviceId, days);
                        if (device == null) return NotFound();
                        return Ok(device);
                    }
                case GraphTypeEnum.Temperature:
                    {
                        var device = _measurementsService.AverageTemperaturesForDeviceGraph(deviceId, days);
                        if (device == null) return NotFound();
                        return Ok(device);
                    }
            }
            return Ok();
        }

    }
}
