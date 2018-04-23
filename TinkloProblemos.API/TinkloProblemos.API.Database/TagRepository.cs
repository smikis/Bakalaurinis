using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Tag;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class TagRepository : ITagRepository
    {
        private readonly string _connectionString;
        public TagRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(CreateTagDto prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TagQueries.Add, prod);
            }
        }

        public IEnumerable<TagDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<TagDto>(TagQueries.GetAll);
            }
        }

        public IEnumerable<TagDto> GetProblemTags(int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<TagDto>(TagQueries.GetProblemTags, new {problemId});
            }
        }

        public int AddToProblem(ProblemTagDto problemTag)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TagQueries.AddToProblem, problemTag);
            }
        }

        public int AddToProblem(IEnumerable<ProblemTagDto> problemTags)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TagQueries.AddToProblem, problemTags);
            }
        }

        public int RemoveFromProblem(ProblemTagDto problemTag)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(TagQueries.RemoveFromProblem, problemTag);
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    var result = dbConnection.Execute(TagQueries.Delete, new { id }, transaction);
                    transaction.Commit();
                    return result;
                }
            }
        }

    }
}
