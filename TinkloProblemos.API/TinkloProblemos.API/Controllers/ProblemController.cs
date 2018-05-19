using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Problem;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Problem")]
    public class ProblemController : Controller
    {
        private readonly IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        // GET: api/Problem
        [HttpGet("{id}")]
        public GetProblem Get(int id)
        {
            return _problemService.GetProblem(id);
        }

        // GET: api/Problem
        [HttpGet("{page}/{pageSize}")]
        public IEnumerable<GetProblem> Get(int page, int pageSize)
        {
            return _problemService.GetProblems(page, pageSize);
        }

        // GET: api/Problem
        [HttpGet("filtered/{page}/{pageSize}")]
        public ProblemPage GetFiltered(int page, int pageSize, [FromQuery] string category, [FromQuery]string status, [FromQuery]string assingnedUser, [FromQuery]string searchTerm, [FromQuery]DateTime? dateFrom, [FromQuery]DateTime? dateTo)
        {
            return _problemService.GetProblems(page, pageSize, category, status, assingnedUser, searchTerm, dateFrom, dateTo);
        }

        // GET: api/Problem
        [HttpGet("filtered")]
        public IEnumerable<GetProblem> GetFiltered([FromQuery] string category, [FromQuery]string status, [FromQuery]string assingnedUser, [FromQuery]DateTime? dateFrom, [FromQuery]DateTime? dateTo)
        {
            return _problemService.GetProblems(category, status, assingnedUser, dateFrom, dateTo);
        }

        // GET: api/Problem
        [HttpGet("user/{userId}")]
        public IEnumerable<GetProblem> GetFiltered([FromQuery] string category, [FromQuery]string status, string userId)
        {
            return _problemService.GetUserProblems(category, status, userId);
        }

        // PUT: api/Problem
        [HttpPut("user/{problemId}")]
        public IActionResult UpdateAssignedUser([FromQuery] string userId, int problemId)
        {
            var result = _problemService.AssignUserToProblem(userId, problemId);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        // PUT: api/Problem
        [HttpPut("description/{problemId}")]
        public IActionResult UpdateDescription([FromQuery] string description, int problemId)
        {
            var result = _problemService.UpdateDescription(description, problemId);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        // PUT: api/Problem
        [HttpPut("internetUser/{problemId}")]
        public IActionResult UpdateAssignedInternetUser([FromQuery] int internetUserId, int problemId)
        {
            var result = _problemService.UpdateInternetUser(internetUserId, problemId);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        // PUT: api/Problem
        [HttpPut("status/{problemId}")]
        public IActionResult UpdateStatus([FromQuery] int statusId, int problemId)
        {
            var result = _problemService.UpdateStatus(statusId, problemId);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }


        // POST: api/Problem
        [HttpPost]
        public IActionResult Post([FromBody]CreateProblem value)
        {
            if (ModelState.IsValid)
            {
                var result = _problemService.Add(value);
                if (result.Success)
                {
                    return Ok(result.Key);
                }
            }
            return BadRequest();
        }
    }
}