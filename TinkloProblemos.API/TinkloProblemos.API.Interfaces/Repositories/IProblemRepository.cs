using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IProblemRepository
    {
        int Add(CreateProblem problem);
        IEnumerable<GetProblem> GetProblems(int skip, int take);
        IEnumerable<GetProblem> GetProblemsFiltered(int skip, int take, string categoryName, string status, string assingnedUser, DateTime dateFrom, DateTime dateTo);
    }
}