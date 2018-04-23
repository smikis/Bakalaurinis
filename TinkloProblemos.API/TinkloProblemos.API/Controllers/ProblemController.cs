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
        // POST: api/Problem
        [HttpPost]
        public IActionResult Post([FromBody]CreateProblem value)
        {
            if (ModelState.IsValid)
            {
                if (_problemService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}