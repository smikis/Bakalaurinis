using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Models;
using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IProblemService
    {
        DatabaseResult Add(CreateProblem createProblem);
        IEnumerable<GetProblem> GetProblems(int page, int pageSize);
        ProblemPage GetProblems(int page, int pageSize, string category, string status, string assingnedUser, string searchTerm, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<GetProblem> GetProblems(string category, string status, string assingnedUser, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<GetProblem> GetUserProblems(string category, string status, string assingnedUser);
        GetProblem GetProblem(int id);
        bool AssignUserToProblem(string userId, int problemId);
        bool UpdateDescription(string description, int problemId);
        bool UpdateInternetUser(int internetUserId, int problemId);
        bool UpdateStatus(int statusId, int problemId);
    }
}