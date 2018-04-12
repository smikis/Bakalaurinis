using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TinkloProblemos.API.Contracts.InternetUser;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class InternetUserRepository : IInternetUserRepository
    {
        private readonly string _connectionString;
        public InternetUserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(InternetUserDto prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(InternetUserQueries.Add, prod);
            }
        }

        public IEnumerable<InternetUserDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<InternetUserDto>(InternetUserQueries.GetAll);
            }
        }

        public IEnumerable<InternetUserDto> GetAll(int skip, int take)
        {         
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<InternetUserDto>(InternetUserQueries.GetPage, new { skip, take } );
            }
        }

        public InternetUserDto GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<InternetUserDto>(InternetUserQueries.GetById, new { id }).FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(InternetUserQueries.Delete, new { Id = id });
            }
        }

        public int Update(InternetUserUpdateDto prod, int categoryId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(InternetUserQueries.Update, new { prod, categoryId });
            }
        }
    }
}
