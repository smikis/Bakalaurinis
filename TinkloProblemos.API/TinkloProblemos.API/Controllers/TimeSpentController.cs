using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Category;
using TinkloProblemos.API.Contracts.TimeSpent;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/TimeSpent")]
    public class TimeSpentController : Controller
    {
        private readonly ITimeSpentService _timeSpentService;
        public TimeSpentController(ITimeSpentService timeSpentService)
        {
            _timeSpentService = timeSpentService;
        }

        // GET: api/problem/TimeSpent/
        [HttpGet("problem/{problemId}")]
        public IEnumerable<GetTimeSpent> GetProblemTimeSpent(int problemId)
        {
            return _timeSpentService.GetProblemTimeSpent(problemId);
        }

        // GET: api/problem/TimeSpent/
        [HttpGet("user/{userId}")]
        public IEnumerable<GetTimeSpent> GetUserTimeSpent(string userId)
        {
            return _timeSpentService.GetUserTimeSpent(userId);
        }

        // POST: api/TimeSpent
        [HttpPost]
        public IActionResult Post([FromBody]CreateTimeSpent value)
        {
            if (ModelState.IsValid)
            {
                if (_timeSpentService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        // PUT: api/TimeSpent/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateTimeSpent value)
        {
            if (ModelState.IsValid)
            {
                if (_timeSpentService.Update(value, id))
                {
                    return Ok();
                }
            }
            return BadRequest();

        }

        // DELETE: api/TimeSpent/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (_timeSpentService.Delete(id))
                {
                    return Ok();
                }
            }
            return BadRequest();

        }
    }
}
