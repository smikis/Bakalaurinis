using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Ping;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class PingRepository : IPingRepository
    {
        private readonly string _connectionString;
        public PingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public IEnumerable<GetPing> GetInternetUserPingInformation(int internetUserId, int limit)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetPing>(PingQueries.GetInternetUserPingResults, new {internetUserId, limit});
            }
        }

        public GetPing GetLastSuccessfullInternetUserPing(int internetUserId, DateTime dateFrom)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.QuerySingleOrDefault<GetPing>(PingQueries.GetLastSuccessfullPing, new {internetUserId, dateFrom });
            }
        }

        public GetPing GetFirstSuccessfullInternetUserPing(int internetUserId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.QuerySingleOrDefault<GetPing>(PingQueries.GetFirstSuccessfullPing, new { internetUserId });
            }
        }

        public GetPing GetLastFailedInternetUserPing(int internetUserId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.QuerySingleOrDefault<GetPing>(PingQueries.GetLastFailedPing, new { internetUserId});
            }
        }

    }
}
