using System.Collections.Generic;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class InternetUserService : IInternetUserService
    {
        private readonly IInternetUserRepository _internetUserRepository;
        public InternetUserService(IInternetUserRepository internetUserRepository)
        {
            _internetUserRepository = internetUserRepository;
        }
        public bool Add(InternetUserDto internetUser)
        {
            if (_internetUserRepository.Add(internetUser) != 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<InternetUserDto> GetAll()
        {
            return _internetUserRepository.GetAll();
        }

        public IEnumerable<InternetUserDto> GetAll(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _internetUserRepository.GetAll(skip, pageSize);
        }

        public InternetUserDto GetById(int id)
        {
            return _internetUserRepository.GetById(id);
        }

        public bool Delete(int id)
        {
            if (_internetUserRepository.Delete(id) != 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(InternetUserUpdateDto internetUser, int internetUserId)
        {
            if (_internetUserRepository.Update(internetUser, internetUserId) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
