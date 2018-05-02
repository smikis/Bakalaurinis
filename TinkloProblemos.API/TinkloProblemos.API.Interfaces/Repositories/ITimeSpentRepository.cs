using System.Collections.Generic;
using TinkloProblemos.API.Contracts.TimeSpent;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface ITimeSpentRepository
    {
        int CreateTimeSpent(CreateTimeSpent timeSpent);
        IEnumerable<GetTimeSpent> GetProblemTimeSpent(int problemId);
        IEnumerable<GetTimeSpent> GetUserTimeSpent(string userId);
        int Delete(int id);
        int Update(UpdateTimeSpent prod, int id);
    }
}