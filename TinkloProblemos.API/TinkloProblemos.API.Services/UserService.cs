using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TinkloProblemos.API.Contracts.User;
using TinkloProblemos.API.Identity.Entities;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<GetUser> GetUsers()
        {
            return _userManager.Users.Select(x=> new GetUser
            {
                Email = x.Email,
                Id = x.Id,
                LastName = x.LastName,
                Name = x.Name
            });
        }

        public IEnumerable<GetUser> GetUsers(int page, int pageSize)
        {
            var skip = (page-1) * pageSize;
            return _userManager.Users.Skip(skip).Take(pageSize).Select(x => new GetUser
            {
                Email = x.Email,
                Id = x.Id,
                LastName = x.LastName,
                Name = x.Name
            });
        }

        public async Task<bool> CreateUserAsync(Register registerModel)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = registerModel.Name,
                LastName = registerModel.LastName,
                Address = registerModel.Address,
                Email = registerModel.Email,
                UserName = registerModel.Email,
                Id = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(applicationUser, registerModel.Password);
            if (result.Succeeded)
            {
                //Add user to role
                return true;
            }
            return false;
        }
    }
}
