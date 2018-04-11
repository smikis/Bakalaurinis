using System.Collections.Generic;
using System.Threading.Tasks;
using TinkloProblemos.API.Contracts.User;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(Register registerModel);
        IEnumerable<GetUser> GetUsers();
        IEnumerable<GetUser> GetUsers(int page, int pageSize);
    }
}