using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TinkloProblemos.API.Contracts.User;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Interfaces.Services.IAuthenticationService _authenticationService;
        private readonly ILogger _logger;
        public LoginController(UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory, SignInManager<ApplicationUser> signInManager, Interfaces.Services.IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<LoginController>();
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, true, true);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "Logged in. Generating token");
                    var user = await _userManager.FindByEmailAsync(login.Email);
                    var token = await _authenticationService.GenerateTokenAsync(user);
                    return Ok(token);
                }
            }
            return BadRequest();
        }
    }
}