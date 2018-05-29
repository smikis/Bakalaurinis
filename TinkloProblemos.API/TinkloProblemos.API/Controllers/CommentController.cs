using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Comment;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/Comment")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, User")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/InternetUser
        [HttpGet]
        public IEnumerable<GetComment> Get()
        {
            return _commentService.GetAll();
        }

        // GET: api/InternetUser
        [HttpGet("{problemId}")]
        public IEnumerable<GetComment> GetProblemComments(int problemId)
        {
            return _commentService.GetAll(problemId);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateComment value)
        {
            if (ModelState.IsValid)
            {
                if (_commentService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

    }
}
