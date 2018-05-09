using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.TimeSpent;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class TimeSpentRepository : ITimeSpentRepository
    {
        private readonly string _connectionString;
        public TimeSpentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int CreateTimeSpent(CreateTimeSpent timeSpent)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    var createdId = dbConnection.ExecuteScalar<int>(TimeSpentQueries.Add, timeSpent, transaction);
                    transaction.Commit();
                    return createdId;
                }

            }
        }

        public IEnumerable<GetTimeSpent> GetProblemTimeSpent(int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetTimeSpent>(TimeSpentQueries.GetProblemTimeSpent, new {problemId});
            }
        }

        public IEnumerable<GetTimeSpent> GetUserTimeSpent(string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetTimeSpent>(TimeSpentQueries.GetUserTimeSpent, new { userId });
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TimeSpentQueries.Delete, new { Id = id });
            }
        }

        public int Update(UpdateTimeSpent prod, int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TimeSpentQueries.Update, new { prod.Description, id, prod.HoursSpent, prod.DateRecorded });
            }
        }

    }
}
