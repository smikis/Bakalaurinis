using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Reports;
using TinkloProblemos.API.Database;

namespace TinkloProblemos.API.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        public IEnumerable<UserReport> GetAll(DateTime dateFrom, DateTime dateTo)
        {

            return _reportsRepository.GetAll(dateFrom, dateTo);
        }

        public IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsRepository.GetTimeConsumingInternetUsers(dateFrom, dateTo, limit);
        }

        public IEnumerable<TimeConsumingProblem> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsRepository.GetTimeConsumingProblems(dateFrom, dateTo, limit);
        }
    }
}
