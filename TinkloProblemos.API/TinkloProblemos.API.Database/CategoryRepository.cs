using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Category;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(CategoryDto prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(CategoryQueries.Add, prod);
            }
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<CategoryDto>(CategoryQueries.GetAll);
            }
        }

        public CategoryDto GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<CategoryDto>(CategoryQueries.GetById, new { Id = id }).FirstOrDefault();
            }
        }

        public int Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(CategoryQueries.Delete, new { Id = id });
            }
        }

        public int Update(CategoryUpdateDto prod, int categoryId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(CategoryQueries.Update, new { prod, categoryId });
            }
        }
    }
}
