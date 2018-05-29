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

        public ProblemPage GetProblemsFiltered(int skip, int take, string categoryName, string status, string assignedUser, int? internetUser, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var count = dbConnection.ExecuteScalar<int>(ProblemQueries.GetFilteredPageCount,
                    new {categoryName, status, assignedUser, dateFrom, dateTo, internetUser });
                var data = dbConnection.Query<GetProblem>(ProblemQueries.GetFilteredPage,
                    new {skip, take, categoryName, status, assignedUser, dateFrom, dateTo, internetUser });
                return new ProblemPage
                {
                    Data = data,
                    Total = count
                };
            }
        }

        public IEnumerable<GetProblem> GetProblemsFiltered(string categoryName, string status, string assignedUser, int? internetUser, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetProblem>(ProblemQueries.GetFiltered, new {categoryName, status, assignedUser, dateFrom, dateTo, internetUser });
            }
        }

        public ProblemPage GetProblemsFilteredSearch(int skip, int take, string categoryName, string status, string assignedUser, int? internetUser, string searchQuery, DateTime? dateFrom, DateTime? dateTo)
        {
            using (IDbConnection dbConnection = Connection)
            {          
                var count = dbConnection.ExecuteScalar<int>(ProblemQueries.GetFilteredSearchCount,
                    new { categoryName, status, assignedUser, dateFrom, dateTo, searchQuery, internetUser });
                var data = dbConnection.Query<GetProblem>(ProblemQueries.GetFilteredSearch, new { skip, take, categoryName, status, assignedUser, dateFrom, dateTo, searchQuery, internetUser });
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
        public int AssignUserToProblem(string userId, int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                try
                {
                    return dbConnection.Execute(ProblemQueries.UpdateAssignedUser, new {userId, problemId});
                }
                catch (Exception e)
                {
                    return 0;
                }
                
            }
        }

        public int UpdateDescription(string description, int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(ProblemQueries.UpdateDescription, new { description, problemId });
            }
        }

        public int UpdateInternetUser(int internetUserId, int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(ProblemQueries.UpdateInternetUser, new { internetUserId, problemId });
            }
        }

        public int UpdateStatus(int statusId, int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(ProblemQueries.UpdateStatus, new { statusId, problemId });
            }
        }

    }
}
