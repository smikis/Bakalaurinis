using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TinkloProblemos.API.Identity
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        private MySqlConnection _sqlConnection;
        private readonly string _connectionString;

        public DatabaseConnectionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<MySqlConnection> CreateConnectionAsync()
        {
            _sqlConnection = new MySqlConnection(_connectionString);
            await _sqlConnection.OpenAsync();

            return await Task.FromResult(_sqlConnection);
        }

        public MySqlConnection CreateConnection()
        {
            _sqlConnection = new MySqlConnection(_connectionString);
            _sqlConnection.Open();

            return _sqlConnection;
        }

        public void Dispose()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}