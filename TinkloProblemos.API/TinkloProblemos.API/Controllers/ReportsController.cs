using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinkloProblemos.API.Contracts.Reports;
using TinkloProblemos.API.Services;

namespace TinkloProblemos.API.Controllers
{
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [Route("api/Reports")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
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
        public IEnumerable<TimeConsumingProblem> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsService.GetTimeConsumingProblems(dateFrom, dateTo, limit);
        }


        [HttpGet("internetUser/{limit}/{dateFrom}/{dateTo}")]
        public IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit)
        {
            return _reportsService.GetTimeConsumingInternetUsers(dateFrom, dateTo, limit);
        }

    }
}