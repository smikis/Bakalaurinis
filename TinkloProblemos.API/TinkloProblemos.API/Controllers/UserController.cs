using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Dapper.Entities;
using Identity.Dapper.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Models;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;

        public UserController(UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = new CustomUser
            {
                UserName = "test",
                Email = "tomas@tomas.lt"
            };
            var result = await _userManager.CreateAsync(user, "test11!");
            if (result.Succeeded)
            {

            }
            return Ok();
        }
    }
}