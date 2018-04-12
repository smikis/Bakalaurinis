﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.User;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{page}/{pageSize}")]
        public  IActionResult Get(int page, int pageSize)
        {
            return  Ok(_userService.GetUsers(page,pageSize));
        }


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
            return BadRequest();
        }
    }
}