using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Devices;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Device")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        // GET: api/Device
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public IEnumerable<DeviceDto> Get()
        {
            return _deviceService.GetAll();
        }

        // GET: api/Device/5
       /* [HttpGet("{id}", Name = "Get")]
        public DeviceDto Get(int id)
        {
            return _deviceService.G;
        }*/
        
        // POST: api/Device
        [HttpPost]
        public IActionResult Post([FromBody]DeviceDto value)
        {

            if (ModelState.IsValid)
            {
                if (_deviceService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        
        // PUT: api/Device/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]DeviceDto value)
        {
            if (ModelState.IsValid)
            {
                if (_deviceService.Update(value, id))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        
    }
}
