using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Reports;

namespace TinkloProblemos.API.Services
{
    public interface IReportsService
    {
        IEnumerable<UserReport> GetAll(DateTime dateFrom, DateTime dateTo);
        IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit);
        IEnumerable<TimeConsumingProblem> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit);
    }
}