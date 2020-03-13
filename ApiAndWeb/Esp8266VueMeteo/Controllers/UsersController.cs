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
    public class UsersController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly IUsersService _usersService;
        private readonly IMeasurementsService _measurementsService;

        public UsersController(IDevicesService devicesService, IUsersService usersService, IMeasurementsService measurementsService)
        {
            _devicesService = devicesService;
            _usersService = usersService;
            _measurementsService = measurementsService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello form Users!");
        }
        [HttpGet("{userName}")]
        public IActionResult GetUserData(string userName)
        {
            return Ok($"Data user: {userName}");
        }

        [HttpGet("{userName}/devices")]
        public IActionResult GetUserDevices(string userName)
        {
            var userId = _usersService.GetUserIdByName(userName);
            if (userId == null) return NotFound("User Not Found!");
            var devices = _devicesService.GetUserDevicesSelectModel(userId.Value);
            return Ok(devices);
        }

        [HttpGet("{userName}/devices/{deviceName}")]
        public IActionResult GetUserDevice(string userName, string deviceName)
        {
            var userId = _usersService.GetUserIdByName(userName);
            if (userId == null) return NotFound("User Not Found!");
            var deviceId = _devicesService.GetDeviceIdByNormalizedName(deviceName);
            if (deviceId == null) return NotFound("Device Not Found!");
            var deviceData = _devicesService.GetDeviceData(deviceId.Value);
            return Ok(deviceData);
        }

        [HttpGet("{userName}/{deviceName}/data.json")]
        public IActionResult GetCurrentDataJson(string userName, string deviceName)
        {
            var userId = _usersService.GetUserIdByName(userName);
            if (userId == null) return NotFound("User Not Found!");
            var deviceId = _devicesService.GetDeviceIdByNormalizedName(deviceName);
            if (deviceId == null) return NotFound("Device Not Found!");
            var averageMeasurements = _measurementsService.GetCurrentDataJson(deviceId.Value);
            return Ok(averageMeasurements);
        }
    }
}
