using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Ping.Database.Models;

namespace Ping.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new MySqlConnection(_connectionString);
        public DatabaseConnection(IConfigurationRoot configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        public IEnumerable<InternetUser> GetInternetUsers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<InternetUser>(Queries.GetInternetUsers);
            }
        }

        public void InsertPingInformation(List<PingResult> results)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(Queries.InsertInternetUser, results);
            }
            Connection.Close();
        }
    }
}
