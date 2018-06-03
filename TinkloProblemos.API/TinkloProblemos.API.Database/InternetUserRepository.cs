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

        public IEnumerable<InternetUserDto> Search(string searchQuery)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<InternetUserDto>(InternetUserQueries.Search, new { searchQuery });
            }
        }

        public InternetUserPage SearchPage(int skip, int take,string searchQuery)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var count = dbConnection.ExecuteScalar<int>(InternetUserQueries.SearchCount,
                    new { searchQuery, skip, take });
                var data =  dbConnection.Query<InternetUserDto>(InternetUserQueries.SearchPage, new { searchQuery, skip,take });
                return new InternetUserPage
                {
                    Data = data,
                    Total = count
                };
            }
        }

        public InternetUserPage GetPage(int skip, int take)
        {         
            using (IDbConnection dbConnection = Connection)
            {
                var count = dbConnection.ExecuteScalar<int>(InternetUserQueries.GetPageCount,
                    new { skip, take });
                var data =  dbConnection.Query<InternetUserDto>(InternetUserQueries.GetPage, new { skip, take } );
                return new InternetUserPage
                {
                    Data = data,
                    Total = count
                };
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

        public int Update(InternetUserUpdateDto prod, int internetUserId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(InternetUserQueries.Update, new { prod.Description, prod.ContractId, prod.FirstName, prod.LastName, prod.IpAddress, prod.Location, prod.StatusId, Id = internetUserId });
            }
        }
    }
}
