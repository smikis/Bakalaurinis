using System.Collections.Generic;
using TinkloProblemos.API.Contracts.TimeSpent;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface ITimeSpentService
    {
        bool Add(CreateTimeSpent timeSpent);
        IEnumerable<GetTimeSpent> GetProblemTimeSpent(int problemId);
        IEnumerable<GetTimeSpent> GetUserTimeSpent(string userId);
        bool Delete(int id);
        bool Update(UpdateTimeSpent timeSpent, int id);
    }
}