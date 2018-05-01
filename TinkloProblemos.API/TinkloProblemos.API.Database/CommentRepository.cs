using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Comment;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;
        public CommentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(CreateComment prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(CommentQueries.Add, prod);
            }
        }

        public IEnumerable<GetComment> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetComment>(CommentQueries.GetAll);
            }
        }

        public IEnumerable<GetComment> GetAll(int problemId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetComment>(CommentQueries.GetAllProblemComments, new { problemId });
            }
        }

    }
}
