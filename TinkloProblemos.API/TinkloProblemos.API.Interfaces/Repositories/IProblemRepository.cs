using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IProblemRepository
    {
        int Add(CreateProblem problem);
        IEnumerable<GetProblem> GetProblems(int skip, int take);
        ProblemPage GetProblemsFiltered(int skip, int take, string categoryName, string status, string assingnedUser, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<GetProblem> GetProblemsUser(string categoryName, string status, string assignedUser);
        IEnumerable<GetProblem> GetProblemsFiltered(string categoryName, string status, string assignedUser, DateTime? dateFrom, DateTime? dateTo);
        ProblemPage GetProblemsFilteredSearch(int skip, int take, string categoryName, string status, string assignedUser, string searchQuery, DateTime? dateFrom, DateTime? dateTo);
    }
}