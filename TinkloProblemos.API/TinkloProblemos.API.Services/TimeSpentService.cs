using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Contracts.TimeSpent;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class TimeSpentService : ITimeSpentService
    {
        private readonly ITimeSpentRepository _timeSpentRepository;
        public TimeSpentService(ITimeSpentRepository timeSpentRepository)
        {
            _timeSpentRepository = timeSpentRepository;
        }
        public DatabaseResult Add(CreateTimeSpent timeSpent)
        {
            var result = _timeSpentRepository.CreateTimeSpent(timeSpent);
            var databaseResult = new DatabaseResult
            {
                Success = false
            };
            if (result != 0)
            {
                databaseResult.Key = result;
                databaseResult.Success = true;
                return databaseResult;
            }
            return databaseResult;
        }

        public IEnumerable<GetTimeSpent> GetProblemTimeSpent(int problemId)
        {
            return _timeSpentRepository.GetProblemTimeSpent(problemId);
        }

        public IEnumerable<GetTimeSpent> GetUserTimeSpent(string userId)
        {
            return _timeSpentRepository.GetUserTimeSpent(userId);
        }

        public bool Delete(int id)
        {
            if (_timeSpentRepository.Delete(id) != 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(UpdateTimeSpent timeSpent, int id)
        {
            if (_timeSpentRepository.Update(timeSpent, id) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
