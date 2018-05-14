using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Location;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [Route("api/Location")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{userId}")]
        public GetLocation Get(string userId)
        {
            return _locationService.Get(userId);
        }

        [HttpPost]
        public bool Post([FromBody] WriteLocation location)
        {
            return _locationService.Add(location);
        }
    }
}