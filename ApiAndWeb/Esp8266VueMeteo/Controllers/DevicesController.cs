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
        
        public DevicesController(IDevicesService devicesService)
        {
            _devicesService = devicesService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello from devices!");
        }

        [HttpGet("{deviceId}")]
        public IActionResult GetDeviceDetails(Guid deviceId)
        {
            var device = _devicesService.GetDeviceData(deviceId);
            if (device == null) return NotFound();
            return Ok(device);
        }
        


    }
}
