using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Location;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class LocationRepository : ILocationRepository
    {
        private readonly string _connectionString;
        public LocationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(WriteLocation prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(LocationQueries.InsertUserCoordinates, prod);
            }
        }
      

        public GetLocation GetByUserId(string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<GetLocation>(LocationQueries.GetCoordinates, new { userId }).FirstOrDefault();
            }
        }

        public int Update(UpdateLocation prod, string userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(LocationQueries.UpdateUserCoordinates, new { prod.Lat, prod.Lng, userId });
            }
        }
    }
}
