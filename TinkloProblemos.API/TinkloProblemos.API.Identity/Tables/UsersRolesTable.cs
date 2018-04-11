using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using TinkloProblemos.API.Identity.Entities;

namespace TinkloProblemos.API.Identity.Tables
{
    public class UsersRolesTable
    {
        private readonly MySqlConnection _sqlConnection;

        public UsersRolesTable(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task AddToRoleAsync(ApplicationUser user, Guid roleId)
        {
            const string command = "INSERT INTO UsersRoles " +
                                   "VALUES (@UserId, @RoleId);";

            return _sqlConnection.ExecuteAsync(command, new
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, Guid roleId)
        {
            const string command = "DELETE " +
                                   "FROM UsersRoles " +
                                   "WHERE UserId = @UserId AND RoleId = @RoleId;";

            return _sqlConnection.ExecuteAsync(command, new
            {
                UserId = user.Id,
                RoleId = roleId
            });
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "SELECT r.Name " +
                                   "FROM Roles as r " +
                                   "INNER JOIN UsersRoles AS ur ON ur.RoleId = r.Id " +
                                   "WHERE ur.UserId = @UserId;";

            IEnumerable<string> userRoles = Task.Run(() => _sqlConnection.QueryAsync<string>(command, new
            {
                UserId = user.Id
            }), cancellationToken).Result;

            return Task.FromResult<IList<string>>(userRoles.ToList());
        }
    }
}