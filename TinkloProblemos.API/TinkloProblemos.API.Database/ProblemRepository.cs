using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Contracts.Problem;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly string _connectionString;
        public ProblemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(CreateProblem problem)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    var createdId = dbConnection.ExecuteScalar<int>(ProblemQueries.Add, problem, transaction);
                    dbConnection.Execute(ProblemCategoryQueries.Add, new {CategoryId = problem.Category, ProblemId = createdId}, transaction);
                    transaction.Commit();
                    return createdId;
                }
                
            }
        }
      
    }
}
