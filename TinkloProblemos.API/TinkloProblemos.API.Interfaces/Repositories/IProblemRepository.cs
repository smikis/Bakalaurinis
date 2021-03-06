﻿using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Problem;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IProblemRepository
    {
        int Add(CreateProblem problem);
        IEnumerable<GetProblem> GetProblems(int skip, int take);
        ProblemPage GetProblemsFiltered(int skip, int take, string categoryName, string status, string assingnedUser, int? internetUser, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<GetProblem> GetProblemsUser(string categoryName, string status, string assignedUser);
        IEnumerable<GetProblem> GetProblemsFiltered(string categoryName, string status, string assignedUser, int? internetUser, DateTime? dateFrom, DateTime? dateTo);
        ProblemPage GetProblemsFilteredSearch(int skip, int take, string categoryName, string status, string assignedUser, int? internetUser, string searchQuery, DateTime? dateFrom, DateTime? dateTo);
        GetProblem GetProblem(int id);
        int AssignUserToProblem(string userId, int problemId);
        int UpdateDescription(string description, int problemId);
        int UpdateInternetUser(int internetUserId, int problemId);
        int UpdateStatus(int statusId, int problemId);
    }
}