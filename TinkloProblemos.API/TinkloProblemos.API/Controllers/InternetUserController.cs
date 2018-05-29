using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/InternetUser")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, User")]
    public class InternetUserController : Controller
    {
        private readonly IInternetUserService _internetUserService;
        public InternetUserController(IInternetUserService internetUserService)
        {
            _internetUserService = internetUserService;
        }

        // GET: api/InternetUser
        [HttpGet]
        public IEnumerable<InternetUserDto> Get()
        {
            return _internetUserService.GetAll();
        }

        // GET: api/InternetUser
        [HttpGet("{page}/{pageSize}")]
        public InternetUserPage Get(int page, int pageSize)
        {
            return _internetUserService.GetAll(page, pageSize);
        }

        // GET: api/InternetUser/5
        [HttpGet("{id}", Name = "Get")]
        public InternetUserDto Get(int id)
        {
            return _internetUserService.GetById(id);
        }

        // GET: api/InternetUser/5
        [HttpGet("search/{searchTerm}")]
        public IEnumerable<InternetUserDto> Search(string searchTerm)
        {
            return _internetUserService.Search(searchTerm);
        }

        // GET: api/Problem
        [HttpGet("filtered/{page}/{pageSize}")]
        public InternetUserPage GetFiltered(int page, int pageSize, [FromQuery]string searchTerm)
        {
            return _internetUserService.GetInternetUsers(page, pageSize, searchTerm);
        }

        // POST: api/InternetUser
        [HttpPost]
        public IActionResult Post([FromBody]InternetUserDto value)
        {
            if (ModelState.IsValid)
            {
                if (_internetUserService.Add(value))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        
        // PUT: api/InternetUser/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]InternetUserUpdateDto value)
        {
            if (ModelState.IsValid)
            {
                if (_internetUserService.Update(value, id))
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
            if (_internetUserService.Delete(id))
            {
                return Ok();
            }    
        return BadRequest();
    }
    }
}
