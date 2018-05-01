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

        public GetProblem GetProblem(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.QueryFirst<GetProblem>(ProblemQueries.GetProblem, new { id });
            }
        }

        public IEnumerable<GetProblem> GetProblems(int skip, int take)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetProblem>(ProblemQueries.GetAllUnfiltered, new {skip, take});
            }
        }

        public ProblemPage GetProblemsFiltered(int skip, int take, string categoryName, string status, string assignedUser, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var count = dbConnection.ExecuteScalar<int>(ProblemQueries.GetFilteredPageCount,
                    new {categoryName, status, assignedUser, dateFrom, dateTo});
                var data = dbConnection.Query<GetProblem>(ProblemQueries.GetFilteredPage,
                    new {skip, take, categoryName, status, assignedUser, dateFrom, dateTo});
                return new ProblemPage
                {
                    Data = data,
                    Total = count
                };
            }
        }

        public IEnumerable<GetProblem> GetProblemsFiltered(string categoryName, string status, string assignedUser, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetProblem>(ProblemQueries.GetFiltered, new {categoryName, status, assignedUser, dateFrom, dateTo });
            }
        }

        public ProblemPage GetProblemsFilteredSearch(int skip, int take, string categoryName, string status, string assignedUser, string searchQuery, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {          
                var count = dbConnection.ExecuteScalar<int>(ProblemQueries.GetFilteredSearchCount,
                    new { categoryName, status, assignedUser, dateFrom, dateTo, searchQuery });
                var data = dbConnection.Query<GetProblem>(ProblemQueries.GetFilteredSearch, new { skip, take, categoryName, status, assignedUser, dateFrom, dateTo, searchQuery });
                return new ProblemPage
                {
                    Data = data,
                    Total = count
                };
            }
        }

        public IEnumerable<GetProblem> GetProblemsUser(string categoryName, string status, string assignedUser)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetProblem>(ProblemQueries.GetFilteredUser, new { categoryName, status, assignedUser});
            }
        }

    }
}
