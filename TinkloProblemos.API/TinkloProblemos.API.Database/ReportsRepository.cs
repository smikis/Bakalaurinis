using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Reports;
using TinkloProblemos.API.Contracts.Tag;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly string _connectionString;
        public ReportsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<UserReport> GetAll(DateTime dateFrom, DateTime dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<UserReport>(ReportQueries.GetUserReports, new {dateFrom, dateTo});
            }
        }

        public IEnumerable<TimeConsumingProblem> GetTimeConsumingProblems(DateTime dateFrom, DateTime dateTo, int limit)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<TimeConsumingProblem>(ReportQueries.GetMostTimeSpentTasks, new { dateFrom, dateTo, limit });
            }
        }

        public IEnumerable<TimeConsumingInternetUsers> GetTimeConsumingInternetUsers(DateTime dateFrom, DateTime dateTo, int limit)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<TimeConsumingInternetUsers>(ReportQueries.GetInternetClientWithMostTimeSpent, new { dateFrom, dateTo, limit });
            }
        }

    }
}
