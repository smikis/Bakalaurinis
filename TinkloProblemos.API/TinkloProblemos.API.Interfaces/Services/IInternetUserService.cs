using System.Collections.Generic;
using TinkloProblemos.API.Contracts.InternetUser;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IInternetUserService
    {
        bool Add(InternetUserDto internetUser);
        IEnumerable<InternetUserDto> GetAll();
        InternetUserPage GetAll(int page, int pageSize);
        InternetUserDto GetById(int id);
        bool Delete(int id);
        bool Update(InternetUserUpdateDto internetUser, int internetUserId);
        IEnumerable<InternetUserDto> Search(string searchQuery);
        InternetUserPage GetInternetUsers(int page, int pageSize, string searchQuery);
    }
}