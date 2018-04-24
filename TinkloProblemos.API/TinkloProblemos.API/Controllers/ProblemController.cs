using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Problem;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
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
        [HttpGet("{page}/{pageSize}")]
        public IEnumerable<GetProblem> Get(int page, int pageSize)
        {
            return _problemService.GetProblems(page, pageSize);
        }

        // GET: api/Problem
        [HttpGet("filtered/{page}/{pageSize}")]
        public IEnumerable<GetProblem> GetFiltered(int page, int pageSize, [FromQuery] string category, [FromQuery]string status, [FromQuery]string assingnedUser, [FromQuery]DateTime dateFrom, [FromQuery]DateTime dateTo)
        {
            return _problemService.GetProblems(page, pageSize, category, status, assingnedUser, dateFrom, dateTo);
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