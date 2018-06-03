using System;
using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Reports;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IReportsService
    {
        IEnumerable<UserReport> GetAll(DateTime dateFrom, DateTime dateTo);
        IEnumerable<TimeConsumingProblem> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit);
        IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit);
    }
}