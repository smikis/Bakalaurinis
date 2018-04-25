using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Identity.Entities;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public AuthenticationService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<TokenResult> GenerateTokenAsync(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim("role", item));
            }
            claims.Add(new Claim("ID", user.Id));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims.ToArray());
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResult
            {
                Email = user.Email,
                Roles = roles,
                Token = generatedToken
            };
        }
    }
}
