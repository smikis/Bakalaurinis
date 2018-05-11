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
            return _userManager.Users.Select(x => new GetUser
            {
                Email = x.Email,
                Id = x.Id,
                LastName = x.LastName,
                Name = x.FirstName
            });
        }

        public IEnumerable<GetUser> GetUsers(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return _userManager.Users.Skip(skip).Take(pageSize).Select(x => new GetUser
            {
                Email = x.Email,
                Id = x.Id,
                LastName = x.LastName,
                Name = x.FirstName
            });
        }

        public IEnumerable<GetUser> SearchUsers(string searchTerm)
        {
            return _userManager.Users.Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower())
                                                || x.LastName.ToLower().Contains(searchTerm.ToLower())
                                                || x.Email.ToLower().Contains(searchTerm.ToLower())).Select(x => new GetUser
                                                {
                                                    Email = x.Email,
                                                    Id = x.Id,
                                                    LastName = x.LastName,
                                                    Name = x.FirstName
                                                });
        }

        public UsersPage SearchUsersPage(int page, int pageSize, string searchTerm)
        {
            var skip = page * pageSize;
            var baseQuery = _userManager.Users;
            if (searchTerm != null)
            {
                baseQuery = baseQuery.Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower())
                                                            || x.LastName.ToLower().Contains(searchTerm.ToLower())
                                                            || x.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            var result = baseQuery.Skip(skip)
            .Take(pageSize)
            .Select(x => new GetUser
            {
                Email = x.Email,
                Id = x.Id,
                LastName = x.LastName,
                Name = x.FirstName
            });
            return new UsersPage
            {
                Data = result,
                Total = baseQuery.Count()
            };
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
                await _userManager.AddToRoleAsync(applicationUser, "User");
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUser(EditUser editUser, string userId)
        {
            var oldUser = await _userManager.FindByIdAsync(userId);
            oldUser.FirstName = editUser.Name;
            oldUser.LastName = editUser.LastName;
            oldUser.Address = editUser.Address;
            oldUser.Email = editUser.Email;
            var result = await _userManager.UpdateAsync(oldUser);
            if (result.Succeeded)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteUser(string userId)
        {
            var oldUser = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(oldUser);
            if (result.Succeeded)
            {
                return true;
            }
            return false;

        }

    }
}
