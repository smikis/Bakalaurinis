using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Reports;
using TinkloProblemos.API.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Reports")]
    public class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;
        public ReportsController(IReportsService timeSpentService)
        {
            _reportsService = timeSpentService;
        }


        [HttpGet("user/{dateFrom}/{dateTo}")]
        public IEnumerable<UserReport> GetProblemTimeSpent(DateTime dateFrom, DateTime dateTo)
        {
            return _reportsService.GetAll(dateFrom, dateTo);
        }


        [HttpGet("problem/{limit}/{dateFrom}/{dateTo}")]
        public IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsService.GetTimeConsumingProblems(dateFrom, dateTo, limit);
        }


        [HttpGet("internetUser/{limit}/{dateFrom}/{dateTo}")]
        public IEnumerable<TimeConsumingProblem> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsService.GetTimeConsumingInternetUsers(dateFrom, dateTo, limit);
        }

    }
}