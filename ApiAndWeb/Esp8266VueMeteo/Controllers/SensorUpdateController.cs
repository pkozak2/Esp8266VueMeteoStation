using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esp8266VueMeteo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Esp8266VueMeteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorUpdateController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok("OK!");
        }

        [HttpPost]
        public IActionResult UpdateData([FromBody]SensorUpdateRequest request)
        {
            return new JsonResult(request);
        }
    }
}