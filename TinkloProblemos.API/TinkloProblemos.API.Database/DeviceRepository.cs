using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Contracts.Devices;
using TinkloProblemos.API.Database.Queries;
using TinkloProblemos.API.Interfaces.Repositories;

namespace TinkloProblemos.API.Database
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly string _connectionString;
        public DeviceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public int Add(DeviceDto prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(DeviceQueries.Add, prod);
            }
        }

        public IEnumerable<DeviceDto> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<DeviceDto>(DeviceQueries.GetAll);
            }
        }

        public IEnumerable<DeviceDto> GetAll(int internetUserId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Query<DeviceDto>(DeviceQueries.GetInternetUserDevices, new { internetUserId });
            }
        }

        public int Update(DeviceDto prod, int deviceId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                return dbConnection.Execute(DeviceQueries.Update, new { prod.Name, prod.Description, prod.MacAddress, prod.WarrantyExpiration, prod.InternetUserId, deviceId });
            }
        }
    }
}
