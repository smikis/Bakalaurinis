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
        IEnumerable<GetProblem> GetProblems(int page, int pageSize, string category, string status, string assingnedUser, DateTime dateFrom, DateTime dateTo);
    }
}