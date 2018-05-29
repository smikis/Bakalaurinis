using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Tag;
using TinkloProblemos.API.Interfaces.Services;
using TinkloProblemos.API.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Tag")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, User")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        // GET: api/Tag
        [HttpGet]
        public IEnumerable<TagDto> Get()
        {
            return _tagService.GetAll();
        }

        // GET: api/Tag
        [HttpGet("problem/{problemId}")]
        public IEnumerable<TagDto> GetProblemTags(int problemId)
        {
            return _tagService.GetProblemTags(problemId);
        }

        // POST: api/Tag
        [HttpPost("{tagId}/problem/{problemId}")]
        public IActionResult AddToProblem(int tagId, int problemId)
        {
            if (_tagService.AddToProblem(tagId, problemId))
            {
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/Tag
        [HttpPost("problem")]
        public IActionResult AddToProblem([FromBody] ProblemTagDto problemTag)
        {
            if (_tagService.AddToProblem(problemTag))
            {
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/Tag
        [HttpPost("problem/{problemId}")]
        public IActionResult Post(int problemId, [FromBody]CreateTagDto value)
        {
            if (ModelState.IsValid)
            {
                var result = _tagService.AddProblemTag(value, problemId);
                if (result!= null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        // POST: api/Tag
        [HttpPost]
        public IActionResult AddProblemTag([FromBody]CreateTagDto value)
        {
            if (ModelState.IsValid)
            {
                if (_tagService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                if (_tagService.Delete(id))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
