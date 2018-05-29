using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.User;
using TinkloProblemos.API.Extensions;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetUsers());
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpGet("{page}/{pageSize}")]
        public  IActionResult Get(int page, int pageSize)
        {
            return  Ok(_userService.GetUsers(page,pageSize));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpGet("{userId}")]
        public async Task<GetUserExtended> GetUser(string userId)
        {
            var user = await _userService.GetUser(userId);
            return user;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        // GET: api/Problem
        [HttpGet("filtered/{page}/{pageSize}")]
        public UsersPage GetFiltered(int page, int pageSize,  [FromQuery]string searchTerm)
        {
            return _userService.SearchUsersPage(page, pageSize, searchTerm);
        }


        [HttpGet("search/{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {
            return Ok(_userService.SearchUsers(searchTerm));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Register registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(registerModel);
                if (result)
                {
                    return Ok("User created");
                }
            }
            return BadRequest(this.GetModelStateErrors());
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EditUser editUser, string id)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(editUser, id);
                if (result)
                {
                    return Ok("User created");
                }
            }
            return BadRequest(this.GetModelStateErrors());
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _userService.DeleteUser(id))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}