using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Ping;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Ping")]
    [EnableCors("CorsPolicy")]
    public class PingController : Controller
    {
        private readonly IPingService _pingService;
        public PingController(IPingService pingService)
        {
            _pingService = pingService;
        }
        // GET: api/Ping
        [HttpGet("{internetUser}")]
        public IEnumerable<GetPing> Get(int internetUser)
        {
            return _pingService.GetLatestInternetUserPings(internetUser);
        }

        // GET: api/Ping/problem
        [HttpGet("information/{internetUser}")]
        public PingInformation GetInternetUserStatus(int internetUser)
        {
            return _pingService.GetInternetUserPingInformation(internetUser);
        }
    }
}