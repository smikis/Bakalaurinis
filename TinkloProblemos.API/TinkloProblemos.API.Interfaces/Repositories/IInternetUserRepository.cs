using System.Collections.Generic;
using TinkloProblemos.API.Contracts.InternetUser;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IInternetUserRepository
    {
        int Add(InternetUserDto prod);
        IEnumerable<InternetUserDto> GetAll();
        IEnumerable<InternetUserDto> GetAll(int skip, int take);
        InternetUserDto GetById(int id);
        int Delete(int id);
        int Update(InternetUserUpdateDto prod, int categoryId);
    }
}